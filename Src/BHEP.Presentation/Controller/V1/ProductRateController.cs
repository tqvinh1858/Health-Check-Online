using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.ProductRate;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class ProductRateController : ApiController
{
    public ProductRateController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ProductRateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProductRates([FromQuery] Query.GetProductRate request)
    {
        var result = await sender.Send(new Query.GetProductRateQuery(
            request.ProductId,
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
    public async Task<IActionResult> ProductRates(Command.CreateProductRateCommand request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }

    [HttpPut("{ProductRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProductRates([FromRoute] int ProductRateId, Command.UpdateProductRateCommand request)
    {
        var updateRate = new Command.UpdateProductRateCommand(
            ProductRateId,
            request.Reason,
            request.Rate
            );

        var result = await sender.Send(updateRate);

        return Ok(result);
    }

    [HttpDelete("{ProductRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProductRates([FromRoute] int ProductRateId)
    {
        var result = await sender.Send(new Command.DeleteProductRateCommand(ProductRateId));

        return Ok(result);
    }
}
