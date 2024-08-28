using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.HealthRecord;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class HealthRecordController : ApiController
{
    public HealthRecordController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<Responses.HealthRecordResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> HealthRecords([FromQuery] Query.GetHealthRecordByUserIdQuery request)
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.HealthRecordResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> HeathRecords(Command.CreateHealthRecordCommand request)
    {
        var result = await sender.Send(request);
        if (!result.IsSuccess)
            HandlerFailure(result);

        return Ok(result);
    }

}
