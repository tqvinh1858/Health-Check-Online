using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Service;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Service.Commands;
public sealed class DeleteServiceCommandHandler : ICommandHandler<Command.DeleteServiceCommand>
{
    private readonly IServiceRepository serviceRepository;

    public DeleteServiceCommandHandler(IServiceRepository serviceRepository)
    {
        this.serviceRepository = serviceRepository;
    }

    public async Task<Result> Handle(Command.DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var serviceExist = await serviceRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException.ServiceIdNotFoundException();

        try
        {
            serviceRepository.Remove(serviceExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
