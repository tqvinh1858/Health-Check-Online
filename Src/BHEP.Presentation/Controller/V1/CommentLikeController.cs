using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.CommentLike;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class CommentLikeController : ApiController
{
    public CommentLikeController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.CommentLikeResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CommentLikes(Command.CreateCommentLikeCommand request)
    {
        var result = await sender.Send(request);

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }

    [HttpDelete("{CommentLikeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CommentLikes([FromRoute] int CommentLikeId)
    {
        var result = await sender.Send(new Command.DeleteCommentLikeCommand(CommentLikeId));

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }
}
