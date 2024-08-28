using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.GeoLocation;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion(1)]
//[Authorize(GeoLocations = "Admin")]
public class GeoLocationController : ApiController
{
    public GeoLocationController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<Responses.GeoLocationResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GeoLocations()
    {
        var result = await sender.Send(new Query.GetGeoLocationQuery());
        return Ok(result);
    }

    [HttpGet("{GeoLocationId}")]
    [ProducesResponseType(typeof(Result<Responses.GeoLocationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GeoLocations([FromRoute] int GeoLocationId)
    {
        var result = await sender.Send(new Query.GetGeoLocationByIdQuery(GeoLocationId));
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Responses.GeoLocationResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GeoLocations(Command.CreateGeoLocationCommand request)
    {
        var result = await sender.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{GeoLocationId}")]
    [ProducesResponseType(typeof(Result<Responses.GeoLocationResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GeoLocations([FromRoute] int GeoLocationId, Command.UpdateGeoLocationCommand request)
    {
        var updateGeoLocation = new Command.UpdateGeoLocationCommand
        (
            GeoLocationId,
            request.Latitude,
            request.Longitude
        );
        var result = await sender.Send(updateGeoLocation);
        return Ok(result);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{GeoLocationId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGeoLocations([FromRoute] int GeoLocationId)
    {
        var result = await sender.Send(new Command.DeleteGeoLocationCommand(GeoLocationId));
        return Ok(result);
    }
}
