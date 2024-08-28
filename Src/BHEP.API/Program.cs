using BHEP.API.DependencyInjection.Extensions;
using BHEP.API.Middleware;
using BHEP.Application.DependencyInjection.Extensions;
using BHEP.Infrastructure.BlobStorage.DependencyInjection.Extensions;
using BHEP.Infrastructure.BlobStorage.DependencyInjection.Options;
using BHEP.Infrastructure.Dapper.DependencyInjection.Extensions;
using BHEP.Infrastructure.DependencyInjection.Extensions;
using BHEP.Infrastructure.PayOS.DependencyInjection.Extensions;
using BHEP.Infrastructure.PayOS.DependencyInjection.Options;
using BHEP.Infrastructure.RabbitMQ.DependencyInjection.Extensions;
using BHEP.Infrastructure.RabbitMQ.DependencyInjection.Options;
using BHEP.Infrastructure.Redis.DependencyInjection.Extensions;
using BHEP.Infrastructure.Redis.DependencyInjection.Options;
using BHEP.Infrastructure.ServiceBus.DependencyInjection.Extensions;
using BHEP.Infrastructure.ServiceBus.DependencyInjection.Options;
using BHEP.Infrastructure.VnPay.DependencyInjection.Extensions;
using BHEP.Infrastructure.VnPay.DependencyInjection.Options.Config;
using BHEP.Persistence.DependencyInjection.Extensions;
using BHEP.Persistence.DependencyInjection.Options;
using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add Serilog
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging
    .ClearProviders()
    .AddSerilog();
builder.Host.UseSerilog();

// API
builder.Services.AddControllers()
    .AddApplicationPart(BHEP.Presentation.AssemblyReference.Assembly);
builder.Services.AddJwtAuthentication(builder.Configuration);

// Versioning
builder.Services.AddVersioningConfiguration();

//Application
builder.Services.AddConfigureMediatR();
builder.Services.AddConfigurationMapper();

//Infrastructure
builder.Services.AddInfrastructureServices();

//Infrastructure.BlobStorage
builder.Services.AddConfigInfrastructureBlobStorage();
builder.Services.ConfigureBlobStorageOptions(
    builder.Configuration.GetSection(nameof(BlobStorageOptions)));

//Infrastructure.Dapper
builder.Services.AddInfrastructureDapper();

//Infrastructure.VnPay
builder.Services.AddConfigInfrastructureVnPay();
builder.Services.Configure<VnPayConfig>(builder.Configuration.GetSection("Vnpay"));


//Infrastructure.PayOS
builder.Services.AddConfigInfrastructurePayOS();
builder.Services.ConfigurePayOSOptions(
    builder.Configuration.GetSection(nameof(PayOSOptions)));

//Infrastructure.RabbitMQ
builder.Services.AddConfigureMassTransitRabbitMQ(
    builder.Configuration.GetSection(nameof(MasstransitOptions)));

builder.Services.AddProblemDetails();

builder.Services.AddCors(options
    => options.AddDefaultPolicy(policyBuilder
        => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services.AddDefaultCorrelationId(options =>
{
    options.RequestHeader =
        options.ResponseHeader =
            options.LoggingScopeKey = "CorrelationId";
    options.UpdateTraceIdentifier = true;
    options.AddToLoggingScope = true;
});

builder.Services.AddHttpLogging(options
    => options.LoggingFields = HttpLoggingFields.All);


//Infrastructure.Redis
builder.Services.AddConfigRedis(
    builder.Configuration.GetSection(nameof(RedisOptions)));

//Infrastructure.ServiceBus
builder.Services.AddConfigInfrastructureServiceBus();
builder.Services.ConfigureServiceBusOptions(
    builder.Configuration.GetSection(nameof(ServiceBusOptions)));

// Persistence
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.ConfigureSqlServerRetryOptions(
    builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));

builder.Services.AddTransient<ExceptionHandlingMiddleware>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || builder.Environment.IsStaging())
app.ConfigureSwagger();
app.UseCorrelationId();

app.UseCors();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}
public partial class Program { }
