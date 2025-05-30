using System.Security.Cryptography;
using System.Text;
using Application.CQRS.Payment.Command.Notify;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("notify")]
        public async Task<IActionResult> Notify()
        {

            Request.EnableBuffering();
            using var reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            Request.Body.Position = 0;

            var signatureHeader = Request.Headers["OpenPayu-Signature"].ToString();

            var command = new NotifyCommand
            {
                RawBody = body,
                SignatureHeader = signatureHeader
            };

            var result=await _mediator.Send(command);
            return result ? Ok() : BadRequest("Invalid signature or data");
        }
    }
}
