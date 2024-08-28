using MassTransit;
using MediatR;

namespace BHEP.Contract.Abstractions.Message;

[ExcludeFromTopology]
public interface IMessage : IRequest
{
    public Guid Id { get; set; }
}
