using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;
using MediatR;

namespace Application.CQRS.Address.Command.Create
{
    public class CreateAddressCommand :AddressDto, IRequest<AddressDetailsDto>
    {
    }
}
