using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTO.Bicycle
{
    public class BicycleCartItem
    {
        public string Name { get; set; } = default!;
        public BicycleSize Size { get; set; }
        public decimal PricePerDay { get; set; }
        public int Quantity { get; set; }
    }
}
