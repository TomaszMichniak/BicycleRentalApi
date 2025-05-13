using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Address.Command.Edit
{
    public class EditAddressCommand : AddressDetailsDto, IRequest<AddressDetailsDto>, IHasId
    {
    }
}
