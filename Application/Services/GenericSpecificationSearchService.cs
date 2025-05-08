using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Pagination;
using AutoMapper;
using Domain.Interfaces;
using Domain.Specification;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class GenericSpecificationSearchService<T, TDto> : IGenericSpecificationSearchService<T, TDto> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IMapper _mapper;

        public GenericSpecificationSearchService(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IPagedResult<TDto>> SearchAsync(
            ISpecification<T> specification,
            int pageNumber,
            int pageSize
            )
        {
            var entities = await _repository.FindBySpecification(specification);

            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);

            var paginationDto = new PaginationDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Paginator<TDto>.Create(dtos, paginationDto);
        }


    }
}
