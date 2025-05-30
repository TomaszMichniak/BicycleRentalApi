using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal TotalPrice { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DeliveryHour { get; set; }
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; } = default!;
        public Guid AddressId { get; set; }
        public Address Address { get; set; } = default!;
        public ICollection<Bicycle> Bicycles { get; set; } = default!;
        public Payment Payment { get; set; } = default!;
    }
}
