using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Device;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.Device.Queries;
public sealed class GetDeviceByUserIdQueryHandler : IQueryHandler<Query.GetDeviceByUserIdQuery, PagedResult<Responses.DeviceResponse>>
{
    private readonly IDeviceRepository deviceRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public GetDeviceByUserIdQueryHandler(
        IDeviceRepository deviceRepository,
        IMapper mapper,
        ApplicationDbContext context)
    {
        this.deviceRepository = deviceRepository;
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<Result<PagedResult<Responses.DeviceResponse>>> Handle(Query.GetDeviceByUserIdQuery request, CancellationToken cancellationToken)
    {
        var Events = await context.User
            .Where(x => x.Id == request.UserId)
            .SelectMany(x => x.CoinTransactions)
            .SelectMany(c => c.Devices)
            .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value).ToListAsync();

        var responses = mapper.Map<List<Responses.DeviceResponse>>(Events);

        var result = PagedResult<Responses.DeviceResponse>.Create(
            responses,
            request.PageIndex.Value,
            request.PageSize.Value,
            Events.Count());

        return Result.Success(result);
    }
}
