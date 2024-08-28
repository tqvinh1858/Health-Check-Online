using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Symptom;
using BHEP.Domain.Abstractions.Repositories;

namespace BHEP.Application.Usecases.V1.Symptom.Queries;
public sealed class GetSymptomQueryHandler : IQueryHandler<Query.GetSymptomQuery, List<Responses.SymptomResponse>>
{
    private readonly ISymptomRepository SymptomRepository;
    private readonly IMapper mapper;

    public GetSymptomQueryHandler(
        ISymptomRepository SymptomRepository,
        IMapper mapper)
    {
        this.SymptomRepository = SymptomRepository;
        this.mapper = mapper;
    }

    public async Task<Result<List<Responses.SymptomResponse>>> Handle(Query.GetSymptomQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = SymptomRepository.FindAll().ToList();

            var response = mapper.Map<List<Responses.SymptomResponse>>(result);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
