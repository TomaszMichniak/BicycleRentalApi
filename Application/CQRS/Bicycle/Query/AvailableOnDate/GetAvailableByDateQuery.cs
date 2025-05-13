using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Bicycle;
using Application.Pagination;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Bicycle.Query.AvailableOnDate
{
    public class GetAvailableByDateQuery : IRequest<IPagedResult<BicycleDetailsDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
      
    }
}
