using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V2.CoinTransaction;
using MassTransit;

namespace BHEP.Application.Usecases.V2.CoinTransaction.Commands;
public sealed class CreateCoinTransactionCommandHandler : ICommandHandler<Command.CreateCoinTransactionCommand>
{
    private readonly IBus bus;

    public CreateCoinTransactionCommandHandler(
        IBus bus)
    {
        this.bus = bus;
    }

    public async Task<Result> Handle(Command.CreateCoinTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var cancelToken = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var endpoint = await bus.GetSendEndpoint(Address<Command.CreateCoinTransactionCommand>()); //send-coin-transaction-command
            await endpoint.Send(request, cancelToken.Token);
            return Result.Success(201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

    }
    private static Uri Address<T>()
            => new($"queue:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
}
