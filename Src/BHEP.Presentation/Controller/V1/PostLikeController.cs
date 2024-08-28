using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.PostLike;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class PostLikeController : ApiController
{
    public PostLikeController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.PostLikeResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostLikes(Command.CreatePostLikeCommand request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }

    [HttpDelete("{PostLikeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostLikes([FromRoute] int PostLikeId)
    {
        var result = await sender.Send(new Command.DeletePostLikeCommand(PostLikeId));

        return Ok(result);
    }
}
