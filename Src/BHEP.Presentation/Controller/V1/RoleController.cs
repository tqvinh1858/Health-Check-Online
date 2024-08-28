using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Role;
using BHEP.Presentation.Abstractions;
using BHEP.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion(1)]
//[Authorize(Roles = "admin")]
public class RoleController : ApiController
{
    public RoleController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [Cache<PagedResult<Responses.RoleCacheResponse>>(20)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.RoleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Roles()
    {
        var result = await sender.Send(new Query.GetRoleQuery(null, null, null, 1, 10));
        return Ok(result);
    }

    [HttpGet("{RoleId}")]
    [ProducesResponseType(typeof(Result<Responses.RoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] int RoleId)
    {
        var result = await sender.Send(new Query.GetRoleByIdQuery(RoleId));
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.RoleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Roles(Command.CreateRoleCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{RoleId}")]
    [ProducesResponseType(typeof(Result<Responses.RoleResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Roles([FromRoute] int RoleId, Command.UpdateRoleCommand request)
    {
        var updateRole = new Command.UpdateRoleCommand
        (
            RoleId,
            request.Name,
            request.Description
        );
        var result = await sender.Send(updateRole);
        return Ok(result);
    }


    [HttpDelete("{RoleId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoles([FromRoute] int RoleId)
    {
        var result = await sender.Send(new Command.DeleteRoleCommand(RoleId));
        return Ok(result);
    }
}
