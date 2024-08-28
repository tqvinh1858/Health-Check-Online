using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.BlogRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.BlogRate.Commands;
public sealed class UpdateBlogRateCommandHandler : ICommandHandler<Command.UpdateBlogRateCommand, Responses.BlogRateResponse>
{
    private readonly IBlogRateRepository BlogRateRepository;
    private readonly IMapper mapper;
    public UpdateBlogRateCommandHandler(
        IBlogRateRepository BlogRateRepository,
        IMapper mapper)
    {
        this.BlogRateRepository = BlogRateRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.BlogRateResponse>> Handle(Command.UpdateBlogRateCommand request, CancellationToken cancellationToken)
    {
        var BlogRateExist = await BlogRateRepository.FindByIdAsync(request.Id.Value)
            ?? throw new BlogRateException.BlogRateIdNotFoundException();

        try
        {
            BlogRateExist.Rate = request.Rate;
            BlogRateExist.UpdatedDate = TimeZones.SoutheastAsia;

            BlogRateRepository.Update(BlogRateExist);
            var BlogRateResponse = mapper.Map<Responses.BlogRateResponse>(BlogRateExist);

            return Result.Success(BlogRateResponse, 202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
