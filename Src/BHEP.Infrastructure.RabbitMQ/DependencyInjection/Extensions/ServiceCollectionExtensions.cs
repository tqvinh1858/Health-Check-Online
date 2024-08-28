using System.Reflection;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.JsonConverters;
using BHEP.Infrastructure.RabbitMQ.DependencyInjection.Options;
using BHEP.Infrastructure.RabbitMQ.PipeObservers;
using CorrelationId.Abstractions;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BHEP.Infrastructure.RabbitMQ.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMassTransitRabbitMQ(this IServiceCollection services, IConfigurationSection section)
    {
        var masstransitConfiguration = new MasstransitOptions();
        section.Bind(masstransitConfiguration);

        services.AddMassTransit(mt =>
        {
            mt.AddConsumers(Assembly.GetExecutingAssembly());

            mt.SetKebabCaseEndpointNameFormatter();

            mt.UsingRabbitMq((context, bus) =>
            {

                bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                {
                    h.Username(masstransitConfiguration.UserName);
                    h.Password(masstransitConfiguration.Password);
                });

                bus.UseNewtonsoftJsonSerializer();

                bus.ConfigureNewtonsoftJsonSerializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());
                    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                    return settings;
                });


                bus.ConfigureNewtonsoftJsonDeserializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());
                    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                    return settings;
                });

                bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                bus.ConnectPublishObserver(new LoggingPublishObserver());
                bus.ConnectSendObserver(new LoggingSendObserver());


                // Configure CorrelationId for send commands
                bus.ConfigureSend(pipe => pipe.AddPipeSpecification(
                    new DelegatePipeSpecification<SendContext<ICommand>>(ctx =>
                    {
                        var accessor = context.GetRequiredService<ICorrelationContextAccessor>();
                        ctx.CorrelationId = new(accessor.CorrelationContext.CorrelationId);
                    })));

                // Configure CorrelationId for publish events forward
                bus.ConfigurePublish(pipe => pipe.AddPipeSpecification(
                       new DelegatePipeSpecification<PublishContext<ICommand>>(ctx
                           => ctx.CorrelationId = ctx.InitiatorId))); // CorrelationId sẽ = thằng trước đó

                bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());

                bus.ConfigureEndpoints(context);

            });
        });
        return services;
    }
}
