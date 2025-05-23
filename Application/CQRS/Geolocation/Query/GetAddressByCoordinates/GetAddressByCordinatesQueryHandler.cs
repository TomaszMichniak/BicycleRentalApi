using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;
using Application.DTO.GeoLocation;
using Application.Services;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Geolocation.Query.GetAddressByCoordinates
{
    public class GetAddressByCordinatesQueryHandler : IRequestHandler<GetAddressByCordinatesQuery, AddressWithDeliveryInfoDto>
    {
        private readonly IGeoLocationService _geoLocationService;
        private readonly IMapper _mapper;

        public GetAddressByCordinatesQueryHandler(IGeoLocationService geoLocationService, IMapper mapper)
        {
            _geoLocationService = geoLocationService;
            _mapper = mapper;
        }

        public async Task<AddressWithDeliveryInfoDto> Handle(GetAddressByCordinatesQuery request, CancellationToken cancellationToken)
        {
            var address = await _geoLocationService.ReverseGeocodeAsync(request.Lat, request.Lng);
            if (address == null)
                return null;

            var isInRange = _geoLocationService.IsWithinDeliveryRange(request.Lat, request.Lng);
            var addressDto = _mapper.Map<AddressDto>(address.Value.address);
            return new AddressWithDeliveryInfoDto
            {
                City = addressDto.City,
                Street = addressDto.Street,
                PostalCode = addressDto.PostalCode,
                IsWithinDeliveryRange = isInRange,
            };
        }
    }
}
