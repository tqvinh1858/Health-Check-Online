using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V3.CoinTransaction;
using BHEP.Infrastructure.ServiceBus.DependencyInjection.Options;
using BHEP.Infrastructure.ServiceBus.Services.IServices;
using Microsoft.Extensions.Options;

namespace BHEP.Application.Usecases.V3.CoinTransaction.Commands;
public sealed class CreateCoinTransactionCommandHandler : ICommandHandler<Command.CreateCoinTransactionCommand>
{
    private readonly IMessagePublisher messagePublisher;
    private readonly ServiceBusOptions serviceBusOptions;

    public CreateCoinTransactionCommandHandler(
        IMessagePublisher messagePublisher,
        IOptions<ServiceBusOptions> opitons)
    {
        this.messagePublisher = messagePublisher;
        serviceBusOptions = opitons.Value;
    }

    public async Task<Result> Handle(Command.CreateCoinTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var properties = new Dictionary<string, object>()
            {
                { "messageType", typeof(Command.CreateCoinTransactionCommand).Name }
            };

            await messagePublisher.Publish<Command.CreateCoinTransactionCommand>(request, serviceBusOptions.QueueName);
            return Result.Success(201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
