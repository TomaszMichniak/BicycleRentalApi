using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Pagination;
using AutoMapper;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Bicycle.Query.GetBySpecification
{
    //TODO OrderBy If needed
    public class GetBySpecificationQueryHandler : IRequestHandler<GetBySpecificationQuery, PageResult<BicycleDto>>
    {
        private readonly IBicycleRepository _bicycleRepository;
        private readonly IMapper _mapper;

        public GetBySpecificationQueryHandler(IBicycleRepository bicycleRepository, IMapper mapper)
        {
            _bicycleRepository = bicycleRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<BicycleDto>> Handle(GetBySpecificationQuery request, CancellationToken cancellationToken)
        {
            Specification<Domain.Entities.Bicycle> specification = new();
            specification = AddCriteria(request, specification);
            //AddOrderBy(request, specification);
            var bicycles = await _bicycleRepository.FindBySpecification(specification);
            var bicyclesDto = _mapper.Map<IEnumerable<BicycleDto>>(bicycles);
            var paginationDto= new PaginationDto
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var pagination = Paginator<BicycleDto>.Create(bicyclesDto, paginationDto);
            return pagination;
        }
        private Specification<Domain.Entities.Bicycle> AddCriteria(GetBySpecificationQuery request, Specification<Domain.Entities.Bicycle> specification)
        {
            if (request.Name != null)
            {
                specification.Criteria.Add(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }
            if (request.IsAvailable!=null)
            {
                specification.Criteria.Add(x => x.IsAvailable == request.IsAvailable);
            }
            if(request.Size != null)
            {
                specification.Criteria.Add(x => x.Size == request.Size);
            }
            if (request.Description != null)
            {
                specification.Criteria.Add(x => x.Description.ToLower().Contains(request.Description.ToLower()));
            }
            return specification;
        }
    }
}
