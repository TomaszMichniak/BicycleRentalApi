using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;
using Application.DTO.GeoLocation;
using MediatR;

namespace Application.CQRS.Geolocation.Query.ValidateAddress
{
    public class GetCoordinatesQuery : AddressDto, IRequest<AddressWithDeliveryInfoDto>
    {
    }
}
