using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Bicycle.Command.Create
{
    public class CreateBicycleCommand : IRequest<BicycleDto>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public BicycleSize Size { get; set; }
        public string ImageUrl { get; set; } = default!;
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;

        public CreateBicycleCommand(string name, string description, BicycleSize size,
            string imageUrl, decimal pricePerDay, bool isAvailable)
        {
            Name = name;
            Description = description;
            Size = size;
            ImageUrl = imageUrl;
            PricePerDay = pricePerDay;
            IsAvailable = isAvailable;
        }
    }
}
