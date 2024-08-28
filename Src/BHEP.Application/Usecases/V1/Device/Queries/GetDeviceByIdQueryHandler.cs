using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Device;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Device.Queries;
public sealed class GetDeviceByIdQueryHandler : IQueryHandler<Query.GetDeviceByIdQuery, Responses.DeviceResponse>
{
    private readonly IDeviceRepository deviceRepository;
    private readonly IMapper mapper;

    public GetDeviceByIdQueryHandler(
        IDeviceRepository deviceRepository,
        IMapper mapper)
    {
        this.deviceRepository = deviceRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.DeviceResponse>> Handle(Query.GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await deviceRepository.FindByIdAsync(request.Id, cancellationToken)
         ?? throw new DeviceException.DeviceIdNotFoundException();

        var resultResponse = new Responses.DeviceResponse(
            result.Id,
            result.ProductId,
            result.TransactionId,
            result.Code,
            result.IsSale,
            result.CreatedDate,
            result.SaleDate
          );

        return Result.Success(resultResponse);
    }
}
