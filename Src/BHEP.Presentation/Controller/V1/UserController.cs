using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Extensions;
using BHEP.Contract.Services.V1.User;
using BHEP.Presentation.Abstractions;
using BHEP.Presentation.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion(1)]
//[Authorize(Roles = "Admin")]
public class UserController : ApiController
{
    private const string cacheKey = "/api/v1/user";
    public UserController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [Cache<PagedResult<Responses.UserGetAllCacheResponse>>(600)]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.UserGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Users([FromQuery] Query.GetUser request)
    {
        var result = await sender.Send(new Query.GetUserQuery(
            request.RoleId,
            request.SearchTerm,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }

    [HttpGet("Doctors")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.UserGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Users([FromQuery] Query.GetDoctor request)
    {
        var result = await sender.Send(new Query.GetDoctorQuery(
            request.SpecialistId,
            request.SearchTerm,
            request.SortColumn,
            SortOrderExtension.ConvertStringToSortOrder(request.SortOrder),
            request.PageIndex,
            request.PageSize));
        return Ok(result);
    }

    [HttpGet("OutstandingDoctors")]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.UserGetAllResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOutstandingDoctors([FromQuery] Query.GetOutstandingDoctor request)
    {
        var result = await sender.Send(new Query.GetOutstandingDoctorQuery(
            request.PageIndex,
            request.PageSize));

        return Ok(result);
    }

    [HttpGet("{UserId}")]
    [ProducesResponseType(typeof(Result<Responses.UserGetByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] int UserId)
    {
        var result = await sender.Send(new Query.GetUserByIdQuery(UserId));
        return Ok(result);
    }


    [HttpPost]
    [RemoveCache(cacheKey)]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Users(Command.CreateUserCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }


    [HttpPut("{UserId}")]
    [RemoveCache(cacheKey)]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Users([FromRoute] int UserId, [FromForm] Command.UpdateUserCommand request)
    {
        var updateUser = new Command.UpdateUserCommand
        (
            UserId,
            request.FullName,
            request.Email,
            request.PhoneNumber,
            request.Gender,
            request.Avatar
        );
        var result = await sender.Send(updateUser);
        return Ok(result);
    }

    [HttpPut("{UserId}/ChangePassword")]
    [RemoveCache(cacheKey)]
    [ProducesResponseType(typeof(Result<Responses.UserResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromRoute] int UserId, Command.ChangePasswordCommand request)
    {
        var updateUser = new Command.ChangePasswordCommand
        (
            UserId,
            request.OldPassword,
            request.NewPassword
        );
        var result = await sender.Send(updateUser);
        return Ok(result);
    }


    [HttpDelete("{UserId}")]
    [RemoveCache(cacheKey)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUsers([FromRoute] int UserId)
    {
        var result = await sender.Send(new Command.DeleteUserCommand(UserId));
        return Ok(result);
    }
}
