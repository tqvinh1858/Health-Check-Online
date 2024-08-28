using MassTransit;
using MediatR;

namespace BHEP.Infrastructure.RabbitMQ.Abstractions;

public abstract class Consumer<TMessage> : IConsumer<TMessage>
    where TMessage : class, Contract.Abstractions.Message.INotification
{
    private readonly ISender Sender;

    protected Consumer(ISender sender)
    {
        Sender = sender;
    }

    public async Task Consume(ConsumeContext<TMessage> context)
        => await Sender.Send(context.Message);
}
