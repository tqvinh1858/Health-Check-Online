using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Major;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Major.Queries;
public sealed class GetMajorByIdQueryHandler : IQueryHandler<Query.GetMajorByIdQuery, Responses.MajorResponse>
{
    private readonly IMajorRepository majorRepository;
    private readonly IMapper mapper;

    public GetMajorByIdQueryHandler(
        IMajorRepository majorRepository,
        IMapper mapper)
    {
        this.majorRepository = majorRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.MajorResponse>> Handle(Query.GetMajorByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await majorRepository.FindByIdAsync(request.Id)
           ?? throw new RoleException.RoleIdNotFoundException();

        var resultResponse = mapper.Map<Responses.MajorResponse>(result);
        return Result.Success(resultResponse);
    }
}
