using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Specialist;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion(1)]
//[Authorize(Roles = "Admin")]
public class SpecialistController : ApiController
{
    public SpecialistController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.SpecialistResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Specialists([FromQuery] Query.GetSpecialist request)
    {
        var result = await sender.Send(new Query.GetSpecialistQuery(
            request.SearchTerm,
            null,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));

        return Ok(result);
    }

    [HttpGet("{SpecialistId}")]
    [ProducesResponseType(typeof(Result<Responses.SpecialistGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Specialists([FromRoute] int SpecialistId)
    {
        var result = await sender.Send(new Query.GetSpecialistByIdQuery(SpecialistId));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.SpecialistResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Specialists(Command.CreateSpecialistCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{SpecialistId}")]
    [ProducesResponseType(typeof(Result<Responses.SpecialistResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Specialists([FromRoute] int SpecialistId, Command.UpdateSpecialistCommand request)
    {
        var updateSpecialist = new Command.UpdateSpecialistCommand
        (
            SpecialistId,
            request.Name,
            request.Description
        );
        var result = await sender.Send(updateSpecialist);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{SpecialistId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSpecialists([FromRoute] int SpecialistId)
    {
        var result = await sender.Send(new Command.DeleteSpecialistCommand(SpecialistId));
        return Ok(result);
    }
}
