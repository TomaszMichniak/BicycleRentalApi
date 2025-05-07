using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Pagination;
using MediatR;

namespace Application.CQRS.Bicycle.Query.GetAll
{
    public class GetAllBicycleQuery : IRequest<PageResult<BicycleDto>>
    {
        public PaginationDto Pagination { get; set; }

        public GetAllBicycleQuery(PaginationDto pagination)
        {
            Pagination = pagination;
        }
    }
}
