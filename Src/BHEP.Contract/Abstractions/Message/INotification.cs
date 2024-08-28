using MassTransit;

namespace BHEP.Contract.Abstractions.Message;

// When use Topic Exchange -> dont use [ExcludeFromTopology]
[ExcludeFromTopology]
public interface INotification : IMessage
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Type { get; set; }
    public Guid TransactionId { get; set; }
}
