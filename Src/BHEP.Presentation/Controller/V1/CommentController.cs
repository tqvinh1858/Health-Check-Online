using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Comment;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class CommentController : ApiController
{
    public CommentController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.CommentGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Comments([FromQuery] Query.GetCommentQuery request)
    {
        var result = await sender.Send(request);

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.CommentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Comments([FromBody] Command.CreateCommentCommand request)
    {
        var result = await sender.Send(request);
        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }



    [HttpPut("{CommentId}")]
    [ProducesResponseType(typeof(Result<Responses.CommentResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Comments([FromRoute] int CommentId, [FromBody] Command.UpdateCommentCommand request)
    {
        var updateBlog = new Command.UpdateCommentCommand
        (
            CommentId,
            request.Content
        );
        var result = await sender.Send(updateBlog);
        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }



    [HttpDelete("{CommentId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Comments([FromRoute] int CommentId)
    {
        var result = await sender.Send(new Command.DeleteCommentCommand(CommentId));
        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }
}
