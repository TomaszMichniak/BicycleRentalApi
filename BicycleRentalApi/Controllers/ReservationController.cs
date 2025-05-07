using Application.CQRS.Address.Command.Create;
using Application.CQRS.Bicycle.Command.Delete;
using Application.CQRS.Reservation.Command.Create;
using Application.CQRS.Reservation.Command.Delete;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
        {
            CreateReservationCommandValidator _validator = new CreateReservationCommandValidator();
            ValidationResult result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        [HttpDelete]
        [Route("{id}")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteReservationCommand(id));
            return NoContent();
        }
    }
}
