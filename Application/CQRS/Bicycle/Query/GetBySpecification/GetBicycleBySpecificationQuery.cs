using Domain.Entities;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Bicycle.Query.GetBySpecification
{
    public class GetBicycleBySpecificationQuery : IRequest<IPagedResult<BicycleDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public OrderBy OrderBy { get; set; } = 0;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public BicycleSize? Size { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? PricePerDay { get; set; }
        public bool? IsAvailable { get; set; }
        
    }
}
