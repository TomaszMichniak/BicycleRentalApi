using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTO.Bicycle
{
    public class BicycleCreate
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public BicycleSize Size { get; set; }
        public string ImageUrl { get; set; } = default!;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
