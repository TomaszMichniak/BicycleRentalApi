using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pagination
{
    public static class Paginator<T>
    {

        public static PageResult<T> Create(IEnumerable<T> query, PaginationDto pagination)
        {
            List<T> items = query
                .Skip(pagination.PageSize * (pagination.PageNumber - 1))
                .Take(pagination.PageSize)
                .ToList();

            return new PageResult<T>(
                items,
                pagination.PageNumber,
                pagination.PageSize,
                query.Count(),
                (int)Math.Ceiling((double)query.Count() / pagination.PageSize)
                );


        }
    }
}
