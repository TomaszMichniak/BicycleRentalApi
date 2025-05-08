using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
    }
}
