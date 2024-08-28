using System.ComponentModel.DataAnnotations;
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.BlogRate;
public static class Command
{
    public record CreateBlogRateCommand(
        int UserId,
        int BlogId,
        [Range(0,5)]
        float Rate) : ICommand<Responses.BlogRateResponse>;

    public record UpdateBlogRateCommand(
        int? Id,
        [Range(0,5)]
        float Rate) : ICommand<Responses.BlogRateResponse>;

    public record DeleteBlogRateCommand(int Id) : ICommand;
}
