using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.WorkProfile;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.WorkProfile.Commands;
public sealed class DeleteWorkProfileCommandHandler : ICommandHandler<Command.DeleteWorkProfileCommand>
{
    private readonly IWorkProfileRepository WorkProfileRepository;
    public DeleteWorkProfileCommandHandler(
        IWorkProfileRepository WorkProfileRepository)
    {
        this.WorkProfileRepository = WorkProfileRepository;
    }
    public async Task<Result> Handle(Command.DeleteWorkProfileCommand request, CancellationToken cancellationToken)
    {
        var WorkProfileExist = await WorkProfileRepository.FindByIdAsync(request.Id)
            ?? throw new WorkProfileException.WorkProfileIdNotFoundException();

        try
        {
            WorkProfileRepository.Remove(WorkProfileExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
