using Application.CQRS.Guest.Command.Create;
using Application.CQRS.Guest.Command.Delete;
using Application.CQRS.Guest.Command.Edit;
using Application.CQRS.Guest.Query;
using Application.CQRS.Reservation.Query.GetBySpecification;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GuestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetBySpecification([FromQuery] GetGuestBySpecificationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> Create([FromBody] CreateGuestCommand command)
        {
            CreateGuestCommandValidator _validator = new CreateGuestCommandValidator();
            ValidationResult result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        [HttpPut]
        //[Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit([FromBody] EditGuestCommand command)
        {
            EditGuestCommandValidator _validator = new EditGuestCommandValidator();
            ValidationResult result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
                return BadRequest(command);
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        [HttpDelete]
        [Route("{id}")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteGuestCommand(id));
            return NoContent();
        }
    }
}
