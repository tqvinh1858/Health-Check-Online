using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ServiceRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.ServiceRate.Commands;
public sealed class DeleteServiceRateCommandHandler : ICommandHandler<Command.DeleteServiceRateCommand>
{
    private readonly IServiceRateRepository ServiceRateRepository;
    public DeleteServiceRateCommandHandler(
        IServiceRateRepository ServiceRateRepository)
    {
        this.ServiceRateRepository = ServiceRateRepository;
    }
    public async Task<Result> Handle(Command.DeleteServiceRateCommand request, CancellationToken cancellationToken)
    {
        var ServiceRateExist = await ServiceRateRepository.FindByIdAsync(request.Id)
            ?? throw new ServiceRateException.ServiceRateIdNotFoundException();

        try
        {
            ServiceRateRepository.Delete(ServiceRateExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
