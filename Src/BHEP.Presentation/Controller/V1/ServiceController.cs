using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Service;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class ServiceController : ApiController
{
    public ServiceController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ServiceGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Services([FromQuery] Query.GetService request)
    {
        var result = await sender.Send(new Query.GetServiceQuery(
            request.SearchTerm,
            request.ServiceType,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }


    [HttpGet("{ServiceId}")]
    [ProducesResponseType(typeof(Result<Responses.ServiceGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Services([FromRoute] int ServiceId)
    {
        var result = await sender.Send(new Query.GetServiceByIdQuery(ServiceId));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.ServiceResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Services([FromForm] Command.CreateServiceCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [Authorize(Roles = "admin")]
    [HttpPut("{ServiceId}")]
    [ProducesResponseType(typeof(Result<Responses.ServiceResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Services([FromRoute] int ServiceId, [FromForm] Command.UpdateServiceCommand request)
    {
        var updateService = new Command.UpdateServiceCommand(
            ServiceId,
            request.Name,
            request.Image,
            request.Type,
            request.Description,
            request.Price,
            request.Duration
            );

        var result = await sender.Send(updateService);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{ServiceId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteServices([FromRoute] int ServiceId)
    {
        var result = await sender.Send(new Command.DeleteServiceCommand(ServiceId));
        return Ok(result);
    }

}
