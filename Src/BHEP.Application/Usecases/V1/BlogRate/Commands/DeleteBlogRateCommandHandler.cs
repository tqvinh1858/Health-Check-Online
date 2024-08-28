using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.BlogRate;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;

namespace BHEP.Application.Usecases.V1.BlogRate.Commands;
public sealed class DeleteBlogRateCommandHandler : ICommandHandler<Command.DeleteBlogRateCommand>
{
    private readonly IBlogRateRepository BlogRateRepository;
    public DeleteBlogRateCommandHandler(
        IBlogRateRepository BlogRateRepository)
    {
        this.BlogRateRepository = BlogRateRepository;
    }
    public async Task<Result> Handle(Command.DeleteBlogRateCommand request, CancellationToken cancellationToken)
    {
        var BlogRateExist = await BlogRateRepository.FindByIdAsync(request.Id)
            ?? throw new BlogRateException.BlogRateIdNotFoundException();

        try
        {
            BlogRateRepository.Delete(BlogRateExist);
            return Result.Success(202);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
