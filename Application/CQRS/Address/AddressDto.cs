using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Address
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
