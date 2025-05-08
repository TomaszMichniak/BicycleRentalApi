using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Specification;

namespace Domain.Interfaces
{
    public interface IGenericSpecificationSearchService<T, TDto> where T : class
    {
        public Task<IPagedResult<TDto>> SearchAsync(
        ISpecification<T> specification,
        int pageNumber,
        int pageSize);
    }
}
