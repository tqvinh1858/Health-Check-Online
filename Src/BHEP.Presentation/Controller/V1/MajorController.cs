using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Major;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class MajorController : ApiController
{
    public MajorController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.MajorResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Majors()
    {
        var result = await sender.Send(new Query.GetMajorQuery(null, null, null, 1, 10));
        return Ok(result);
    }


    [HttpGet("{MajorId}")]
    [ProducesResponseType(typeof(Result<Responses.MajorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] int MajorId)
    {
        var result = await sender.Send(new Query.GetMajorByIdQuery(MajorId));
        return Ok(result);
    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.MajorResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Roles(Command.CreateMajorCommand request)
    {
        var result = await sender.Send(request);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(new { message = result.Message });
        }
    }
    [Authorize(Roles = "admin")]
    [HttpPut("{MajorId}")]
    [ProducesResponseType(typeof(Result<Responses.MajorResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] int MajorId, Command.UpdateMajorCommand request)
    {
        var updateRole = new Command.UpdateMajorCommand
        (
            MajorId,
            request.Name,
            request.Description
        );
        var result = await sender.Send(updateRole);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{MajorId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoles([FromRoute] int MajorId)
    {
        var result = await sender.Send(new Command.DeleteMajorCommand(MajorId));
        return Ok(result);
    }

}
