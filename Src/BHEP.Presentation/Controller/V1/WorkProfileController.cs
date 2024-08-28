using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.WorkProfile;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion(1)]
//[Authorize(Roles = "Admin")]
public class WorkProfileController : ApiController
{
    public WorkProfileController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{UserId}")]
    [ProducesResponseType(typeof(Result<Responses.WorkProfileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> WorkProfiles([FromRoute] int UserId)
    {
        var result = await sender.Send(new Query.GetWorkProfileByUserIdQuery(UserId));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.WorkProfileResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> WorkProfiles(Command.CreateWorkProfileCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{WorkProfileId}")]
    [ProducesResponseType(typeof(Result<Responses.WorkProfileResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> WorkProfiles([FromRoute] int WorkProfileId, Command.UpdateWorkProfileCommand request)
    {
        var updateWorkProfile = new Command.UpdateWorkProfileCommand
        (
            WorkProfileId,
            request.MajorId,
            request.SpecialistId,
            request.WorkPlace,
            request.Certificate,
            request.ExperienceYear,
            request.Price
        );
        var result = await sender.Send(updateWorkProfile);
        return Ok(result);
    }


    [HttpDelete("{WorkProfileId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWorkProfiles([FromRoute] int WorkProfileId)
    {
        var result = await sender.Send(new Command.DeleteWorkProfileCommand(WorkProfileId));
        return Ok(result);
    }
}
