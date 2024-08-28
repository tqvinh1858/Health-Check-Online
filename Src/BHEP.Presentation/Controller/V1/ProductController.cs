using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Product;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion("1")]
public class ProductController : ApiController
{
    public ProductController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ProductGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Services([FromQuery] Query.GetProduct request)
    {
        var result = await sender.Send(new Query.GetProductQuery(
            request.SearchTerm,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }


    [HttpGet("{ProductId}")]
    [ProducesResponseType(typeof(Result<Responses.ProductGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Services([FromRoute] int ProductId)
    {
        var result = await sender.Send(new Query.GetProductByIdQuery(ProductId));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.ProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Services([FromForm] Command.CreateProductCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{ProductId}")]
    [ProducesResponseType(typeof(Result<Responses.ProductResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Services([FromRoute] int ProductId, [FromForm] Command.UpdateProductCommand request)
    {
        var updateService = new Command.UpdateProductCommand(
            ProductId,
            request.Name,
            request.Image,
            request.Description,
            request.Price,
            request.Stock
            );

        var result = await sender.Send(updateService);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{ProductId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteServices([FromRoute] int ProductId)
    {
        var result = await sender.Send(new Command.DeleteProductCommand(ProductId));
        return Ok(result);
    }
}
