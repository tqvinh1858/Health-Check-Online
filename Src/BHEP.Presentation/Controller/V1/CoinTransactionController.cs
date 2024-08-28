using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.CoinTransaction;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion("1")]
public class CoinTransactionController : ApiController
{
    public CoinTransactionController(ISender sender) : base(sender)
    {
    }

    [HttpGet()]
    [ProducesResponseType(typeof(Result<Responses.CoinTransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CoinTransactions([FromQuery] Query.GetCoinTransactionQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpGet("{CoinTransactionId}")]
    [ProducesResponseType(typeof(Result<Responses.CoinTransactionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CoinTransactions([FromRoute] int CoinTransactionId)
    {
        var result = await sender.Send(new Query.GetCoinTransactionByIdQuery(CoinTransactionId));
        return Ok(result);
    }

    [HttpGet("UserId")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.CoinTransactionResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GeoLocations([FromQuery] Query.GetCoinTransactionByUserIdQuery request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.CoinTransactionResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CoinTransactions(Command.CreateCoinTransactionCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
}
