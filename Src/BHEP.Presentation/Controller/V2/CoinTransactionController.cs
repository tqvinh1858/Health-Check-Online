using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V2.CoinTransaction;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V2;

[ApiVersion("2")]
public class CoinTransactionController : ApiController
{
    public CoinTransactionController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CoinTransactions(Command.CreateCoinTransactionCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }
}
