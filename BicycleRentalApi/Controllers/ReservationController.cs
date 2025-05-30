using Application.CQRS.Bicycle.Query.GetBySpecification;
using Application.CQRS.Payment.Command.Create;
using Application.CQRS.Reservation.Command.Create;
using Application.CQRS.Reservation.Command.CreateReservationWithTransaction;
using Application.CQRS.Reservation.Command.Delete;
using Application.CQRS.Reservation.Command.Edit;
using Application.CQRS.Reservation.Query.GetBySpecification;
using Application.DTO.Address;
using Application.DTO.Payment;
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
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetBySpecification([FromQuery] GetReservationBySpecificationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
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
        [HttpPost]
        [Route("CreateReservationWithTransaction")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateReservationWithTransaction([FromBody] CreateReservationWithTransactionCommand command)
        {
            //Reservation
            CreateReservationWithTransactionCommandValidator _validator = new CreateReservationWithTransactionCommandValidator();
            ValidationResult result = await _validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var reservationDto = await _mediator.Send(command);
            //Payment
            var createPaymentCommand = new CreatePaymentCommand
            {
                ReservationId = reservationDto.Id,
                CustomerIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Description = $"Rezerwacja nr {reservationDto.Id}",
                TotalAmount = reservationDto.TotalPrice,
                Buyer = new BuyerDto
                {
                    Email = command.Guest.Email,
                    FirstName = command.Guest.FirstName,
                    LastName = command.Guest.LastName,
                    Language = "pl"
                },
                Products = command.Bicycles.Select(b => new ProductDto
                {
                    Name = b.Name,
                    UnitPrice = (b.PricePerDay * 100).ToString("F0"),
                    Quantity = b.Quantity.ToString()
                }).ToList()
            };

            var paymentUrl = await _mediator.Send(createPaymentCommand);

            return Ok(new
            {
                PaymentUrl = paymentUrl
            });
        }
        [HttpPut]
        //[Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit([FromBody] EditReservationCommand command)
        {
            EditReservationCommandValidator _validator = new EditReservationCommandValidator();
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
            await _mediator.Send(new DeleteReservationCommand(id));
            return NoContent();
        }
    }
}
