using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.UserRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.UserRate.Commands;
public sealed class UpdateUserRateCommandHandler : ICommandHandler<Command.UpdateUserRateCommand, Responses.UserRateResponse>
{
    private readonly IUserRateRepository UserRateRepository;
    private readonly IMapper mapper;
    public UpdateUserRateCommandHandler(
        IUserRateRepository UserRateRepository,
        IMapper mapper)
    {
        this.UserRateRepository = UserRateRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.UserRateResponse>> Handle(Command.UpdateUserRateCommand request, CancellationToken cancellationToken)
    {
        var UserRateExist = await UserRateRepository.FindByIdAsync(request.Id.Value)
            ?? throw new UserRateException.UserRateIdNotFoundException();

        try
        {
            UserRateExist.Reason = request.Reason;
            UserRateExist.Rate = request.Rate;
            UserRateExist.UpdatedDate = DateTime.Now;

            UserRateRepository.Update(UserRateExist);
            var UserRateResponse = mapper.Map<Responses.UserRateResponse>(UserRateExist);

            return Result.Success(UserRateResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
