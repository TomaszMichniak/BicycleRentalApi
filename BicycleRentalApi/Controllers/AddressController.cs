using Application.CQRS.Address.Command.Create;
using Application.CQRS.Address.Command.Delete;
using Application.CQRS.Address.Command.Edit;
using Application.CQRS.Address.Query.GetBySpecification;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetBySpecification([FromQuery] GetAddressBySpecificationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetPickupPoints")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPickupPoints()
        {
            var command = new GetAddressBySpecificationQuery() { addressType = Domain.Entities.AddressType.PickupPoint };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
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
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditAddressCommand command)
        {
            EditAddressCommandValidator _validator = new EditAddressCommandValidator();
            ValidationResult result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
                return BadRequest(command);
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteAddressCommand(id));
            return NoContent();
        }
    }
}
