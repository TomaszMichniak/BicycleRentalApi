using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.GeoLocation;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Geolocation.Query.ValidateAddress
{
    public class GetCoordinatesQueryHandler : IRequestHandler<GetCoordinatesQuery, AddressWithDeliveryInfoDto>
    {
        private readonly IGeoLocationService _geoLocationService;
        private readonly IMapper _mapper;

        public GetCoordinatesQueryHandler(IGeoLocationService geoLocationService, IMapper mapper)
        {
            _geoLocationService = geoLocationService;
            _mapper = mapper;
        }

        public async Task<AddressWithDeliveryInfoDto> Handle(GetCoordinatesQuery request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Domain.Entities.Address>(request);
            var result = await _geoLocationService.GetCoordinatesAsync(address);
            if (result is null)
                return null;

            var (standardizedAddress, lat, lng) = result.Value;

            var isInRange = _geoLocationService.IsWithinDeliveryRange(lat, lng);

            var addressDto = _mapper.Map<Domain.Entities.Address>(standardizedAddress);
            var response = new AddressWithDeliveryInfoDto
            {
                City = addressDto.City,
                Street = addressDto.Street,
                PostalCode = addressDto.PostalCode,
                IsWithinDeliveryRange = isInRange,
            };

            return response;
        }
    }
}
