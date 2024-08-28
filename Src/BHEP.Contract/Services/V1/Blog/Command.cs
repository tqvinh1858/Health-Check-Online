
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace BHEP.Contract.Services.V1.Blog;
public class Command
{
    public record CreateBlogCommand(
        int UserId,
        string Title,
        string Content,
        ICollection<int>? Topics,
        ICollection<IFormFile>? Photos) : ICommand<Responses.BlogResponse>;

    public record UpdateBlogCommand(
        int? Id,
        string Title,
        string Content,
        BlogStatus Status,
        ICollection<int>? Topics,
        ICollection<IFormFile>? Photos) : ICommand<Responses.BlogResponse>;

    public record DeleteBlogCommand(int Id) : ICommand;
}
