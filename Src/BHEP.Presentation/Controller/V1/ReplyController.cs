using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Reply;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class ReplyController : ApiController
{
    public ReplyController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ReplyGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Comments([FromQuery] Query.GetReplyQuery request)
    {
        var result = await sender.Send(request);

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.ReplyResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Blogs([FromBody] Command.CreateReplyCommand request)
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
            HandlerFailure(result);
        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }



    [HttpPut("{CommentId}")]
    [ProducesResponseType(typeof(Result<Responses.ReplyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Blogs([FromRoute] int CommentId, [FromBody] Command.UpdateReplyCommand request)
    {
        var updateBlog = new Command.UpdateReplyCommand
        (
            CommentId,
            request.Content
        );
        var result = await sender.Send(updateBlog);
        if (!result.IsSuccess)
            HandlerFailure(result);
        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }



    [HttpDelete("{CommentId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBlogs([FromRoute] int CommentId)
    {
        var result = await sender.Send(new Command.DeleteReplyCommand(CommentId));
        if (!result.IsSuccess)
            HandlerFailure(result);
        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }
}
