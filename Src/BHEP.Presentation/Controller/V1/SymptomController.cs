using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Symptom;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion(1)]
//[Authorize(Roles = "Admin")]
public class SymptomController : ApiController
{
    public SymptomController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.SymptomResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Symptoms()
    {
        var result = await sender.Send(new Query.GetSymptomQuery());

        return Ok(result);
    }

    [HttpGet("{SymptomId}")]
    [ProducesResponseType(typeof(Result<Responses.SymptomResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Symptoms([FromRoute] int SymptomId)
    {
        var result = await sender.Send(new Query.GetSymptomByIdQuery(SymptomId));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.SymptomResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Symptoms(Command.CreateSymptomCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{SymptomId}")]
    [ProducesResponseType(typeof(Result<Responses.SymptomResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Symptoms([FromRoute] int SymptomId, Command.UpdateSymptomCommand request)
    {
        var updateSymptom = new Command.UpdateSymptomCommand
        (
            SymptomId,
            request.Name,
            request.Description
        );
        var result = await sender.Send(updateSymptom);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{SymptomId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSymptoms([FromRoute] int SymptomId)
    {
        var result = await sender.Send(new Command.DeleteSymptomCommand(SymptomId));
        return Ok(result);
    }
}
