using BHEP.Contract.Abstractions.Shared;
using MediatR;

namespace BHEP.Contract.Abstractions.Message;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

