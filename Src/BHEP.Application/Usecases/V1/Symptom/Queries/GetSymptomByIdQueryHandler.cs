using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Symptom;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Symptom.Queries;
public sealed class GetSymptomByIdQueryHandler : IQueryHandler<Query.GetSymptomByIdQuery, Responses.SymptomResponse>
{
    private readonly ISymptomRepository SymptomRepository;
    private readonly IMapper mapper;

    public GetSymptomByIdQueryHandler(ISymptomRepository SymptomRepository, IMapper mapper)
    {
        this.SymptomRepository = SymptomRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.SymptomResponse>> Handle(Query.GetSymptomByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await SymptomRepository.FindByIdAsync(request.Id)
            ?? throw new SymptomException.SymptomIdNotFoundException();

        var resultResponse = mapper.Map<Responses.SymptomResponse>(result);
        return Result.Success(resultResponse);
    }
}
