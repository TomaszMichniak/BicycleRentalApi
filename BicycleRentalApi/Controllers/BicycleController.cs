using Application.CQRS.Bicycle;
using Application.CQRS.Bicycle.Command.Create;
using Application.CQRS.Bicycle.Command.Delete;
using Application.CQRS.Bicycle.Command.Edit;
using Application.CQRS.Bicycle.Query.GetAll;
using Application.CQRS.Bicycle.Query.GetBySpecification;
using Application.CQRS.GenericHandlers;
using Application.Pagination;
using Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BicycleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBicycles([FromQuery] PaginationDto pagination)
        {
            var result = await _mediator.Send(new GetAllBicycleQuery(pagination));
            return Ok(result);
        }
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetBySpecification([FromQuery] GetBySpecificationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> Create([FromBody] CreateBicycleCommand command)
        {
            CreateBicycleCommandValidator _validator = new CreateBicycleCommandValidator();
            ValidationResult result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        //[HttpPut]
        ////[Authorize(Roles ="admin")]
        //public async Task<IActionResult> Edit([FromBody] EditBicycleCommand command)
        //{
        //    EditBicycleCommandValidator _validator = new Edit();
        //    ValidationResult result = await _validator.ValidateAsync(command);
        //    if (!result.IsValid)
        //        return BadRequest(command);
        //    var data = await _mediator.Send(command);
        //    return Ok(data);
        //}
        [HttpDelete]
        [Route("{id}")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteBicycleCommand(id));
            return NoContent();
        }

    }
}
