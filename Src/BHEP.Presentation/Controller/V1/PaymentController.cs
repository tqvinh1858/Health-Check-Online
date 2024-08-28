using Asp.Versioning;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Payment;
using BHEP.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class PaymentController : ApiController
{
    public PaymentController(ISender sender) : base(sender)
    {
    }


    [HttpPost("VNPay")]
    [ProducesResponseType(typeof(Result<Responses.PaymentResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Payments([FromBody] Command.CreatePaymentCommand request)
    {
        var result = await sender.Send(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(new { message = result.Message });
        }
    }

    [HttpPost("PayOS")]
    [ProducesResponseType(typeof(Result<Responses.PayOSResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Payments([FromBody] Command.CreatePayOSLinkCommand request)
    {
        var result = await sender.Send(request);

        if (!result.IsSuccess)
            HandlerFailure(result);
        return Ok(result);
    }



    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Result<Responses.PaymentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        var query = new Query.GetPaymentByIdQuery(id);
        var result = await sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return NotFound(new { message = result.Message });
        }
    }

    [HttpPut("{PaymentId}")]
    [ProducesResponseType(typeof(Result<Responses.PaymentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VnPayReturn([FromRoute] int PaymentId, [FromBody] Command.UpdatePaymentCommand query)
    {
        var command = new Command.UpdatePaymentCommand(
            PaymentId,
            query.Status
        );

        var result = await sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(new { message = result.Message });
        }
    }



    [HttpDelete("{PaymentId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Payments([FromRoute] int PaymentId)
    {
        var result = await sender.Send(new Command.DeletePaymentCommand(PaymentId));
        return Ok(result);
    }


}
