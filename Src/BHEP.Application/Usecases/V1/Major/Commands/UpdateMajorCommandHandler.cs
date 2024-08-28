using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Major;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Major.Commands;
public sealed class UpdateMajorCommandHandler : ICommandHandler<Command.UpdateMajorCommand, Responses.MajorResponse>
{
    private readonly IMajorRepository majorRepository;
    private readonly IMapper mapper;

    public UpdateMajorCommandHandler(
        IMajorRepository majorRepository,
        IMapper mapper)
    {
        this.majorRepository = majorRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.MajorResponse>> Handle(Command.UpdateMajorCommand request, CancellationToken cancellationToken)
    {
        var major = await majorRepository.FindByIdAsync(request.Id, cancellationToken)
           ?? throw new RoleException.RoleIdNotFoundException();

        try
        {
            major.Update(request.Name, request.Description);
            majorRepository.Update(major);

            var resultResponse = mapper.Map<Responses.MajorResponse>(major);
            return Result.Success(resultResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
