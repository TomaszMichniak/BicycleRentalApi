using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pagination
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public PageResult(List<T> items, int pageIndex, int pageSize, int totalCount, int totalPages)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }
    }
}
