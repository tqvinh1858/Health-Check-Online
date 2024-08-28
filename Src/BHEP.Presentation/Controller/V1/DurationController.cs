using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Duration;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion("1")]
public class DurationController : ApiController
{
    public DurationController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.DurationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Services([FromQuery] Query.GetDurationQuery request)
    {
        var result = await sender.Send(new Query.GetDurationQuery(
            request.UserId,
            request.pageIndex,
            request.pageSize));
        return Ok(result);
    }
}
