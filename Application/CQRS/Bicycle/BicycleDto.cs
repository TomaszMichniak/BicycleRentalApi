using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.CQRS.Bicycle
{
    public class BicycleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public BicycleSize Size { get; set; }
        public string ImageUrl { get; set; } = default!;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}
