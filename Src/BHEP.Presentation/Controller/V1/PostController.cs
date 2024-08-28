using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Post;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class PostController : ApiController
{
    public PostController(ISender sender) : base(sender)
    {
    }


    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.PostGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Blogs([FromQuery] Query.GetPost request)
    {
        var result = await sender.Send(new Query.GetPostQuery(
            request.searchTerm,
            request.sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.sortOrder),
            request.pageIndex,
            request.pageSize));

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }

    [HttpGet("{PostId}")]
    [ProducesResponseType(typeof(Result<Responses.PostGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Blogs([FromRoute] int PostId)
    {
        var result = await sender.Send(new Query.GetPostByIdQuery(PostId));

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.PostResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Blogs([FromBody] Command.CreatePostCommand request)
    {
        var result = await sender.Send(request);

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);

    }



    [HttpPut("{PostId}")]
    [ProducesResponseType(typeof(Result<Responses.PostResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Blogs([FromRoute] int PostId, [FromBody] Command.UpdatePostCommand request)
    {
        var updateBlog = new Command.UpdatePostCommand
        (
            PostId,
            request.Specialists,
            request.Title,
            request.Content,
            request.Age,
            request.Gender,
            request.Status
        );
        var result = await sender.Send(updateBlog);

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }



    [HttpDelete("{PostId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBlogs([FromRoute] int PostId)
    {
        var result = await sender.Send(new Command.DeletePostCommand(PostId));

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }
}
