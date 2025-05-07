using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<T> DeleteAsync(T entity);
        public Task<T?> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> IsExist(Guid id);
        public Task<IEnumerable<T>> FindBySpecification(ISpecification<T> specification = null);
        public IQueryable<T> FindBySpecificationQuery(ISpecification<T> specification = null);

    }
}

