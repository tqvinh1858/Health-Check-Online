using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Blog;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class BlogController : ApiController
{
    public BlogController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.BlogGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Blogs([FromQuery] Query.GetBlog request)
    {
        var result = await sender.Send(new Query.GetBlogQuery(
            request.searchTerm,
            request.sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.sortOrder),
            request.pageIndex,
            request.pageSize));
        return Ok(result);
    }

    [HttpGet("{BlogId}")]
    [ProducesResponseType(typeof(Result<Responses.BlogGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Blogs([FromRoute] int BlogId)
    {
        var result = await sender.Send(new Query.GetBlogByIdQuery(BlogId));
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.BlogResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Blogs([FromForm] Command.CreateBlogCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{BlogId}")]
    [ProducesResponseType(typeof(Result<Responses.BlogResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Blogs([FromRoute] int BlogId, [FromForm] Command.UpdateBlogCommand request)
    {
        var updateBlog = new Command.UpdateBlogCommand
        (
            BlogId,
            request.Title,
            request.Content,
            request.Status,
            request.Topics,
            request.Photos
        );
        var result = await sender.Send(updateBlog);
        return Ok(result);
    }


    [HttpDelete("{BlogId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBlogs([FromRoute] int BlogId)
    {
        var result = await sender.Send(new Command.DeleteBlogCommand(BlogId));
        return Ok(result);
    }
}
