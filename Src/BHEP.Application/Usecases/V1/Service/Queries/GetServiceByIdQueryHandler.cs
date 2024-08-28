using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Service;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.Service.Queries;
public sealed class GetServiceByIdQueryHandler : IQueryHandler<Query.GetServiceByIdQuery, Responses.ServiceGetByIdResponse>
{
    private readonly IServiceRepository serviceRepository;
    private readonly IServiceRateRepository serviceRateRepository;
    private readonly IMapper mapper;

    public GetServiceByIdQueryHandler(
        IServiceRepository serviceRepository,
        IMapper mapper,
        IServiceRateRepository serviceRateRepository)
    {
        this.serviceRepository = serviceRepository;
        this.mapper = mapper;
        this.serviceRateRepository = serviceRateRepository;
    }

    public async Task<Result<Responses.ServiceGetByIdResponse>> Handle(Query.GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await serviceRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException.ServiceIdNotFoundException();

        // GetListRating
        var listRating = await serviceRateRepository.FindAll(predicate: x => x.ServiceId == request.Id).ToListAsync();

        // GetAvgRating
        var avgRate = await serviceRateRepository.GetAvgRating(result.Id);

        var resultResponse = new Responses.ServiceGetByIdResponse(
            result.Id,
            result.Image,
            result.Name,
            result.Type,
            result.Description,
            result.Price,
            result.Duration,
            avgRate,
            mapper.Map<List<Contract.Services.V1.ServiceRate.Responses.ServiceRateResponse>>(listRating));

        return Result.Success(resultResponse);
    }
}
