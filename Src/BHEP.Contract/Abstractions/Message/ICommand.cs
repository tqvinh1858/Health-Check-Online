using BHEP.Contract.Abstractions.Shared;
using MassTransit;
using MediatR;

namespace BHEP.Contract.Abstractions.Message;
[ExcludeFromTopology]
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
