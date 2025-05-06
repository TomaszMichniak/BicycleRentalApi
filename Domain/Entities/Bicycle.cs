using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bicycle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public BicycleSize Size { get; set; }
        public string ImageUrl { get; set; } = default!;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Reservation> Reservations { get; set; } = default!;
    }

    public enum BicycleSize
    {
        S,
        M,
        L,
        XL
    }
}

