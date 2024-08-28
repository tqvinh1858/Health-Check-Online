using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Voucher;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion("1")]
public class VoucherController : ApiController
{
    public VoucherController(ISender sender) : base(sender)
    {
    }



    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.VoucherResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Devices([FromQuery] Query.GetVoucher request)
    {
        var result = await sender.Send(new Query.GetVoucherQuery(
            request.SearchTerm,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }


    [HttpGet("{VoucherId}")]
    [ProducesResponseType(typeof(Result<Responses.VoucherResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Devices([FromRoute] int VoucherId)
    {
        var result = await sender.Send(new Query.GetVoucherByIdQuery(VoucherId));
        return Ok(result);
    }

    [HttpGet("User/{UserId}")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.VoucherResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VouchersOfUser(
       [FromRoute] int UserId,
       int? PageIndex = 1,
       int? PageSize = 10)
    {
        var result = await sender.Send(new Query.GetVoucherByUserIdQuery(UserId, PageIndex, PageSize));
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.VoucherResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CoinTransaction(Command.CreateVoucherCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{VoucherId}")]
    [ProducesResponseType(typeof(Result<Responses.VoucherResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDevice([FromRoute] int VoucherId, [FromBody] Command.UpdateVoucherCommand request)
    {
        var updateDevice = new Command.UpdateVoucherCommand(
            VoucherId,
            request.Name,
            request.Code,
            request.Discount,
            request.Stock,
            request.StartDate,
            request.EndDate
        );

        var result = await sender.Send(updateDevice);
        return Ok(result);
    }


    [HttpDelete("{VoucherId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDevice([FromRoute] int VoucherId)
    {
        var result = await sender.Send(new Command.DeleteVoucherCommand(VoucherId));
        return Ok(result);
    }
}
