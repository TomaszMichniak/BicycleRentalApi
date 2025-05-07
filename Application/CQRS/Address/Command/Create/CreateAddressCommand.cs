using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.CQRS.Address.Command.Create
{
    public class CreateAddressCommand : IRequest<AddressDto>
    {
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;

        public CreateAddressCommand(string street, string city, string postalCode)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }
    }
}
