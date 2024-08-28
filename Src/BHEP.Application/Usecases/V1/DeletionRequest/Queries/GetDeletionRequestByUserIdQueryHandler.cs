using System.Linq.Expressions;
using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.DeletionRequest;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Entities.AppointmentEntities;
using BHEP.Domain.Entities;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;
using BHEP.Persistence.Repositories.AppointmentRepo;
using static BHEP.Contract.Services.V1.Major.Responses;

namespace BHEP.Application.Usecases.V1.DeletionRequest.Queries;
public sealed class GetDeletionRequestByUserIdQueryHandler : IQueryHandler<Query.GetDeletionRequestByUserIdQuery, Responses.DeletionRequestResponse>
{
    private readonly IDeletionRequestRepository deletionRequestRepository;
    private readonly IUserRepository userRepository;

    public GetDeletionRequestByUserIdQueryHandler(IDeletionRequestRepository deletionRequestRepository, IUserRepository userRepository)
    {
        this.deletionRequestRepository = deletionRequestRepository;
        this.userRepository = userRepository;
    }

    public async Task<Result<Responses.DeletionRequestResponse>> Handle(Query.GetDeletionRequestByUserIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.UserEntities.DeletionRequest, object>> includeProperty = x => x.User;

        var deletionRequestExist = await deletionRequestRepository.FindByUserIdAsync(request.UserId, cancellationToken, includeProperty)
            ?? throw new DeletionRequestException.DeletionRequestIdNotFoundException();

        var user = await userRepository.FindByIdAsync(request.UserId)
            ?? throw new UserException.UserNotFoundException();



        var response = new Responses.DeletionRequestResponse(
            deletionRequestExist.Id,
            deletionRequestExist.UserId,
            deletionRequestExist.Reason ?? "",
            deletionRequestExist.Status,
            deletionRequestExist.CreatedDate,
            deletionRequestExist.ProccessedDate
            );

        return Result.Success( response );
    } 
}
