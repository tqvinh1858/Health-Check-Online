using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Device;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion(1)]
public class DeviceController : ApiController
{
    public DeviceController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.DeviceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Devices([FromQuery] Query.GetDevice request)
    {
        var result = await sender.Send(new Query.GetDeviceQuery(
            request.SearchTerm,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }


    [HttpGet("{DeviceId}")]
    [ProducesResponseType(typeof(Result<Responses.DeviceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Devices([FromRoute] int DeviceId)
    {
        var result = await sender.Send(new Query.GetDeviceByIdQuery(DeviceId));
        return Ok(result);
    }

    [HttpGet("User/{UserId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.DeviceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DevicesOfUser(
        [FromRoute] int UserId,
        int? PageIndex = 1,
        int? PageSize = 10)
    {
        var result = await sender.Send(new Query.GetDeviceByUserIdQuery(UserId, PageIndex, PageSize));
        return Ok(result);
    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.DeviceResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDevice([FromBody] Command.CreateDeviceCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
    [Authorize(Roles = "admin")]
    [HttpPut("{DeviceId}")]
    [ProducesResponseType(typeof(Result<Responses.DeviceResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDevice([FromRoute] int DeviceId, [FromBody] Command.UpdateDeviceCommand request)
    {
        var updateDevice = new Command.UpdateDeviceCommand(
            DeviceId,
            request.ProductId,
            request.TransactionId,
            request.Code,
            request.IsSale,
            request.SaleDate
        );

        var result = await sender.Send(updateDevice);
        return Ok(result);
    }
    [Authorize(Roles = "admin")]
    [HttpDelete("{DeviceId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDevice([FromRoute] int DeviceId)
    {
        var result = await sender.Send(new Command.DeleteDeviceCommand(DeviceId));
        return Ok(result);
    }
}
