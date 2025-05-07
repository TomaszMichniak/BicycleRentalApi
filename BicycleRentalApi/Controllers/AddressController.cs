using Application.CQRS.Address.Command.Create;
using Application.CQRS.Address.Command.Delete;
using Application.CQRS.Bicycle.Command.Delete;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
        {
            CreateAddressCommandValidator _validator = new CreateAddressCommandValidator();
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
            await _mediator.Send(new DeleteAddressCommand(id));
            return NoContent();
        }
    }
}
