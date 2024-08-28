using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Auth;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion("1")]
public class AuthController : ApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(Result<Responses.UserAuthenticated>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login(Query.Login request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("RefreshToken")]
    [ProducesResponseType(typeof(Result<Responses.UserAuthenticated>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(Query.Token request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }

    [HttpPost("ForgetPassword")]
    public async Task<IActionResult> ForgetPassword(Command.ForgetPasswordCommand request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }


}
