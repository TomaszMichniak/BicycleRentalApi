using Application.CQRS.Geolocation.Query.GetAddressByCoordinates;
using Application.CQRS.Geolocation.Query.ValidateAddress;
using Application.DTO.Address;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GeolocationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetCordinates")]
        public async Task<IActionResult> GetCordinates([FromBody] GetCoordinatesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("GetAddressByCordinates")]
        public async Task<IActionResult> GetAddress([FromBody] GetAddressByCordinatesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
