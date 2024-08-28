using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.UserRate;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class UserRateController : ApiController
{
    public UserRateController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.UserRateResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UserRates([FromQuery] Query.GetUser request)
    {
        var result = await sender.Send(new Query.GetUserRateQuery(
            request.EmployeeId,
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
    public async Task<IActionResult> UserRates(Command.CreateUserRateCommand request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }

    [HttpPut("{UserRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UserRates([FromRoute] int UserRateId, Command.UpdateUserRateCommand request)
    {
        var updateRate = new Command.UpdateUserRateCommand(
            UserRateId,
            request.Reason,
            request.Rate
            );

        var result = await sender.Send(updateRate);

        return Ok(result);
    }

    [HttpDelete("{UserRateId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UserRates([FromRoute] int UserRateId)
    {
        var result = await sender.Send(new Command.DeleteUserRateCommand(UserRateId));

        return Ok(result);
    }
}
