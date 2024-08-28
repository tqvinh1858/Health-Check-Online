using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Appointment;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class AppointmentController : ApiController
{
    public AppointmentController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.AppointmentGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Appointments([FromQuery] Query.GetAll request)
    {
        var result = await sender.Send(new Query.GetAppointmentQuery(
            request.Status,
            request.UserId,
            request.SearchTerm,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }

    [HttpGet("{Range}/CustomerInRange")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.AppointmentGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Appointments([FromQuery] Query.GetByRange request)
    {
        var result = await sender.Send(new Query.GetAppointmentInRangeQuery(
            request.Range,
            request.Latitude,
            request.Longitude,
            request.SearchTerm,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }

    [HttpGet("{AppointmentId}")]
    [ProducesResponseType(typeof(Result<Responses.AppointmentGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Appointments([FromRoute] int AppointmentId)
    {
        var result = await sender.Send(new Query.GetAppointmentByIdQuery(AppointmentId));
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.AppointmentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Appointments(Command.CreateAppointmentCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{AppointmentId}")]
    [ProducesResponseType(typeof(Result<Responses.AppointmentResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Appointments([FromRoute] int AppointmentId, Command.UpdateAppointmentCommand request)
    {
        var updateAppointment = new Command.UpdateAppointmentCommand
        (
            AppointmentId,
            request.Date,
            request.Time,
            request.Price,
            request.Address,
            request.Latitude,
            request.Longitude,
            request.Description,
            request.Note,
            request.Status,
            request.Symptoms
        );
        var result = await sender.Send(updateAppointment);
        return Ok(result);
    }

    [HttpPut("{AppointmentId}/Status")]
    [ProducesResponseType(typeof(Result<Responses.AppointmentResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> JobApplications([FromRoute] int AppointmentId, Command.UpdateAppointmentStatusCommand request)
    {
        var updateJobApplication = new Command.UpdateAppointmentStatusCommand
        (
            AppointmentId,
            request.CustomerId,
            request.EmployeeId,
            request.Status
        );
        var result = await sender.Send(updateJobApplication);
        return Ok(result);
    }

    [HttpDelete("{AppointmentId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAppointments([FromRoute] int AppointmentId)
    {
        var result = await sender.Send(new Command.DeleteAppointmentCommand(AppointmentId));
        return Ok(result);
    }
}
