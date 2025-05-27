using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public AddressType Type { get; set; } = AddressType.GuestAddress;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
    public enum AddressType
    {
        PickupPoint,
        GuestAddress
    }
}
