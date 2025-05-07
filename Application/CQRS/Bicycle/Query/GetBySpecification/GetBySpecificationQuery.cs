using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Pagination;
using Domain.Entities;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Bicycle.Query.GetBySpecification
{
    public class GetBySpecificationQuery : IRequest<PageResult<BicycleDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public BicycleSize? Size { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? PricePerDay { get; set; }
        public bool? IsAvailable { get; set; }
        public OrderBy OrderBy { get; set; } = 0;
        
    }
}
