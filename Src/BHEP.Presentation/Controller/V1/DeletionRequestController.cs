using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.DeletionRequest;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;

[ApiVersion("1")]
public class DeletionRequestController : ApiController
{
    public DeletionRequestController(ISender sender) : base(sender)
    {
    }


    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.DeletionRequestResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletionRequest([FromQuery] Query.GetDeletionRequest request)
    {
        var result = await sender.Send(new Query.GetDeletionRequestQuery(
            request.SearchTerm,
            request.Status,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }


    [HttpGet("{DeletionRequestId}")]
    [ProducesResponseType(typeof(Result<Responses.DeletionRequestGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletionRequest([FromRoute] int DeletionRequestId)
    {
        var result = await sender.Send(new Query.GetDeletionRequestByIdQuery(DeletionRequestId));
        return Ok(result);
    }


    [HttpGet("User/{UserId}")]
    [ProducesResponseType(typeof(Result<Responses.DeletionRequestResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletionRequestByUserId([FromRoute] int UserId)
    {
        var result = await sender.Send(new Query.GetDeletionRequestByUserIdQuery(UserId));
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return NotFound(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.DeletionRequestResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletionRequest([FromBody] Command.CreateDeletionRequestCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }



    [HttpPut("{DeletionRequestId}")]
    [ProducesResponseType(typeof(Result<Responses.DeletionRequestResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletionRequest([FromRoute] int DeletionRequestId, [FromBody] Command.UpdateDeletionRequestCommand request)
    {
        var updateBlog = new Command.UpdateDeletionRequestCommand
        (
            DeletionRequestId,
            request.UserId,
            request.Reason,
            request.Status,
            request.ProccessedDate
        );
        var result = await sender.Send(updateBlog);
        return Ok(result);
    }

    [HttpDelete("{DeletionRequestId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeletionRequest([FromRoute] int DeletionRequestId)
    {
        var result = await sender.Send(new Command.DeleteDeletionRequestCommand(DeletionRequestId));
        return Ok(result);
    }
}
