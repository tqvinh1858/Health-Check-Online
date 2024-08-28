using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.Topic;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class TopicController : ApiController
{
    public TopicController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.TopicResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Topics([FromQuery] Query.GetTopic request)
    {
        var result = await sender.Send(new Query.GetTopicQuery(
            request.searchTerm,
            request.sortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.sortOrder),
            request.pageIndex,
            request.pageSize));
        return Ok(result);
    }

    [HttpGet("{TopicId}")]
    [ProducesResponseType(typeof(Result<Responses.TopicResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Topics([FromRoute] int TopicId)
    {
        var result = await sender.Send(new Query.GetTopicByIdQuery(TopicId));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.TopicResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Topics(Command.CreateTopicCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{TopicId}")]
    [ProducesResponseType(typeof(Result<Responses.TopicResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Topics([FromRoute] int TopicId, Command.UpdateTopicCommand request)
    {
        var updateTopic = new Command.UpdateTopicCommand
        (
            TopicId,
            request.Name,
            request.Description
        );
        var result = await sender.Send(updateTopic);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{TopicId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTopics([FromRoute] int TopicId)
    {
        var result = await sender.Send(new Command.DeleteTopicCommand(TopicId));
        return Ok(result);
    }
}
