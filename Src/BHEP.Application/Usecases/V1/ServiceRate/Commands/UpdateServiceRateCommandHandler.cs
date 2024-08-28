using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.ServiceRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.ServiceRate.Commands;
public sealed class UpdateServiceRateCommandHandler : ICommandHandler<Command.UpdateServiceRateCommand, Responses.ServiceRateResponse>
{
    private readonly IServiceRateRepository ServiceRateRepository;
    private readonly IMapper mapper;
    public UpdateServiceRateCommandHandler(
        IServiceRateRepository ServiceRateRepository,
        IMapper mapper)
    {
        this.ServiceRateRepository = ServiceRateRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.ServiceRateResponse>> Handle(Command.UpdateServiceRateCommand request, CancellationToken cancellationToken)
    {
        var ServiceRateExist = await ServiceRateRepository.FindByIdAsync(request.Id.Value)
            ?? throw new ServiceRateException.ServiceRateIdNotFoundException();

        try
        {
            ServiceRateExist.Reason = request.Reason;
            ServiceRateExist.Rate = request.Rate;
            ServiceRateExist.UpdatedDate = TimeZones.SoutheastAsia;

            ServiceRateRepository.Update(ServiceRateExist);
            var ServiceRateResponse = mapper.Map<Responses.ServiceRateResponse>(ServiceRateExist);

            return Result.Success(ServiceRateResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
