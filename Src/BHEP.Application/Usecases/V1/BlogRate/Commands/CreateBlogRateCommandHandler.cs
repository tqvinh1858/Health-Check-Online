using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.BlogRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.BlogRate.Commands;
public sealed class CreateBlogRateCommandHandler : ICommandHandler<Command.CreateBlogRateCommand, Responses.BlogRateResponse>
{
    private readonly IBlogRateRepository BlogRateRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public CreateBlogRateCommandHandler(
        IBlogRateRepository BlogRateRepository,
        ApplicationDbContext context,
        IMapper mapper)
    {
        this.BlogRateRepository = BlogRateRepository;
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.BlogRateResponse>> Handle(Command.CreateBlogRateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check AlreadyRating?
            var BlogRateExist = await BlogRateRepository.FindAll(x => x.BlogId == request.BlogId && x.UserId == request.UserId).FirstOrDefaultAsync();
            if (BlogRateExist != null)
                throw new BlogRateException.BlogRateBadRequestException("Already Rating");

            var BlogRate = new Domain.Entities.BlogEntities.BlogRate
            {
                UserId = request.UserId,
                BlogId = request.BlogId,
                Rate = request.Rate,
            };

            BlogRateRepository.Add(BlogRate);
            await context.SaveChangesAsync();

            var response = mapper.Map<Responses.BlogRateResponse>(BlogRate);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
