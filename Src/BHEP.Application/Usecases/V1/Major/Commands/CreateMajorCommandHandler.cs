using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Major;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.Major.Commands;
public sealed class CreateMajorCommandHandler : ICommandHandler<Command.CreateMajorCommand, Responses.MajorResponse>
{
    private readonly IMajorRepository majorRepository;
    private readonly IMapper mapper;

    public CreateMajorCommandHandler(
        IMajorRepository majorRepository,
        IMapper mapper)
    {
        this.majorRepository = majorRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.MajorResponse>> Handle(Command.CreateMajorCommand request, CancellationToken cancellationToken)
    {
        var majorNameExist = majorRepository.FindAll(x => x.Name == request.Name)
                    ?? throw new MajorException.MajorBadRequestException("Major Name is already exist");

        var major = new Domain.Entities.Major
        {
            Name = request.Name,
            Description = request.Description,
        };

        try
        {
            majorRepository.Add(major);

            var resultResponse = mapper.Map<Responses.MajorResponse>(major);
            return Result.Success(resultResponse, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
