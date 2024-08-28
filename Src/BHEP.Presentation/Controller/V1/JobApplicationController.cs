using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.JobApplication;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class JobApplicationController : ApiController
{
    public JobApplicationController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyCollection<Responses.JobApplicationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> JobApplications([FromQuery] Query.GetJobApplication request)
    {
        var result = await sender.Send(new Query.GetJobApplicationQuery(
            request.SearchTerm,
            request.Status,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }

    [HttpGet("{JobApplicationId}")]
    [ProducesResponseType(typeof(Result<Responses.JobApplicationGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> JobApplications([FromRoute] int JobApplicationId)
    {
        var result = await sender.Send(new Query.GetJobApplicationByIdQuery(JobApplicationId));
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.JobApplicationResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> JobApplications([FromForm] Command.CreateJobApplicationCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{JobApplicationId}")]
    [ProducesResponseType(typeof(Result<Responses.JobApplicationResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> JobApplications([FromRoute] int JobApplicationId, [FromForm] Command.UpdateJobApplicationCommand request)
    {
        var updateJobApplication = new Command.UpdateJobApplicationCommand
        (
            JobApplicationId,
            request.MajorId,
            request.FullName,
            request.Certification,
            request.Avatar,
            request.WorkPlace,
            request.ExperienceYear
        );
        var result = await sender.Send(updateJobApplication);
        return Ok(result);
    }

    [HttpPut("{JobApplicationId}/Status")]
    [ProducesResponseType(typeof(Result<Responses.JobApplicationResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> JobApplications([FromRoute] int JobApplicationId, Command.UpdateJobApplicationStatusCommand request)
    {
        var updateJobApplication = new Command.UpdateJobApplicationStatusCommand
        (
            JobApplicationId,
            request.CustomerId,
            request.SpecialistId,
            request.RoleId,
            request.Description,
            request.Status,
            request.CancelReason
        );
        var result = await sender.Send(updateJobApplication);
        return Ok(result);
    }


    [HttpDelete("{JobApplicationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteJobApplications([FromRoute] int JobApplicationId)
    {
        var result = await sender.Send(new Command.DeleteJobApplicationCommand(JobApplicationId));
        return Ok(result);
    }
}
