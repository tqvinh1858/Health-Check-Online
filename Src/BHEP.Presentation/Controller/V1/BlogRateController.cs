using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.BlogRate;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class BlogRateController : ApiController
{
    public BlogRateController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BlogRates(Command.CreateBlogRateCommand request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }

    [HttpPut("{BlogRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BlogRates([FromRoute] int BlogRateId, Command.UpdateBlogRateCommand request)
    {
        var updateRate = new Command.UpdateBlogRateCommand(
            BlogRateId,
            request.Rate
            );

        var result = await sender.Send(updateRate);

        return Ok(result);
    }

    [HttpDelete("{BlogRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BlogRates([FromRoute] int BlogRateId)
    {
        var result = await sender.Send(new Command.DeleteBlogRateCommand(BlogRateId));

        return Ok(result);
    }
}
