using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.ProductService;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class ProductServiceController : ApiController
{
    public ProductServiceController(ISender sender) : base(sender)
    {
    }


    [HttpGet("Outstanding")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.OutstandingResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProductService([FromQuery] Query.GetOutstandingQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.ProductServiceResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProductService([FromQuery] Query.GetProductService request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
}
