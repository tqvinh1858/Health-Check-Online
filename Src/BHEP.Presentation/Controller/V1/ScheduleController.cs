using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Schedule;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class ScheduleController : ApiController
{
    public ScheduleController(ISender sender) : base(sender)
    {
    }

    [HttpGet("Employee/{EmployeeId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ScheduleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Schedules([FromRoute] int EmployeeId)
    {
        var result = await sender.Send(new Query.GetScheduleByIdQuery(EmployeeId));
        return Ok(result);
    }


    [HttpGet("Date/{Date}")]
    [ProducesResponseType(typeof(Result<Responses.ScheduleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Schedules([FromRoute] string Date)
    {
        var result = await sender.Send(new Query.GetScheduleByDateQuery(Date));
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else if (result.StatusCode == 404)
        {
            return NotFound(new { message = result.Message });
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = result.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.ScheduleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Schedules([FromBody] Command.CreateScheduleCommand request)
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



    [HttpPut("{EmployeeId}/{Date}")]
    [ProducesResponseType(typeof(Result<Responses.ScheduleResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Schedules([FromRoute] int EmployeeId, [FromRoute] string Date, [FromBody] Command.UpdateScheduleCommand request)
    {
        var updateSchedule = new Command.UpdateScheduleCommand
        (
            EmployeeId,
            Date,
            request.Time
        );
        var result = await sender.Send(updateSchedule);
        return Ok(result);
    }


    [HttpDelete("{ScheduleId}/{EmployeeId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSchedules([FromRoute] int ScheduleId, [FromRoute] int EmployeeId)
    {
        var result = await sender.Send(new Command.DeleteScheduleCommand(ScheduleId, EmployeeId));
        return Ok(result);
    }
}
