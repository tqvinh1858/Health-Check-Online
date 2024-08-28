using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.ServiceRate;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class ServiceRateController : ApiController
{
    public ServiceRateController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ServiceRateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ServiceRates([FromQuery] Query.GetServiceRate request)
    {
        var result = await sender.Send(new Query.GetServiceRateQuery(
            request.ServiceId,
            null,
            null,
            SortOrderExtension.ConvertStringToSortOrder(null),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ServiceRates(Command.CreateServiceRateCommand request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }

    [HttpPut("{ServiceRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ServiceRates([FromRoute] int ServiceRateId, Command.UpdateServiceRateCommand request)
    {
        var updateRate = new Command.UpdateServiceRateCommand(
            ServiceRateId,
            request.Reason,
            request.Rate
            );

        var result = await sender.Send(updateRate);

        return Ok(result);
    }

    [HttpDelete("{ServiceRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ServiceRates([FromRoute] int ServiceRateId)
    {
        var result = await sender.Send(new Command.DeleteServiceRateCommand(ServiceRateId));

        return Ok(result);
    }
}
