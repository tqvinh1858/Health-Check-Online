using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ReplyLike;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class ReplyLikeController : ApiController
{
    public ReplyLikeController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.ReplyLikeResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReplyLikes(Command.CreateReplyLikeCommand request)
    {
        var result = await sender.Send(request);

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }

    [HttpDelete("{ReplyLikeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReplyLikes([FromRoute] int ReplyLikeId)
    {
        var result = await sender.Send(new Command.DeleteReplyLikeCommand(ReplyLikeId));

        return result.IsSuccess ? Ok(result) : HandlerFailure(result);
    }
}
