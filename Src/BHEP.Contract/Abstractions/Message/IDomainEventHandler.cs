using MediatR;

namespace BHEP.Contract.Abstractions.Message;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
{
}
