using BHEP.Contract.Abstractions.Shared;
using MediatR;

namespace BHEP.Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
