namespace BHEP.Contract.Abstractions.Message;
public interface IDomainEvent : MediatR.INotification
{
    public Guid Id { get; init; } // init khoi tao 1 lan ban dau
}
