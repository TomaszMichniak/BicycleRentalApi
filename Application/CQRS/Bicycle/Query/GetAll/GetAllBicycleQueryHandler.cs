using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Pagination;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Bicycle.Query.GetAll
{
    public class GetAllBicycleQueryHandler : IRequestHandler<GetAllBicycleQuery, PageResult<BicycleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBicycleRepository _bicycleRepository;

        public GetAllBicycleQueryHandler(IMapper mapper, IBicycleRepository bicycleRepository)
        {
            _mapper = mapper;
            _bicycleRepository = bicycleRepository;
        }

        public async Task<PageResult<BicycleDto>> Handle(GetAllBicycleQuery request, CancellationToken cancellationToken)
        {
            var bicycles = await _bicycleRepository.GetAllAsync();
            var bicyclesDto = _mapper.Map<IEnumerable<BicycleDto>>(bicycles);
            var pagination = Paginator<BicycleDto>.Create(bicyclesDto, request.Pagination);
            return pagination;
        }
    }
}
