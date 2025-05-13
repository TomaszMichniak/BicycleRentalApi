using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Bicycle;
using Application.Pagination;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Bicycle.Query.GetAll
{
    public class GetAllBicycleQueryHandler : IRequestHandler<GetAllBicycleQuery, PageResult<BicycleDetailsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBicycleRepository _bicycleRepository;

        public GetAllBicycleQueryHandler(IMapper mapper, IBicycleRepository bicycleRepository)
        {
            _mapper = mapper;
            _bicycleRepository = bicycleRepository;
        }

        public async Task<PageResult<BicycleDetailsDto>> Handle(GetAllBicycleQuery request, CancellationToken cancellationToken)
        {
            var bicycles = await _bicycleRepository.GetAllAsync();
            var bicyclesDto = _mapper.Map<IEnumerable<BicycleDetailsDto>>(bicycles);
            var pagination = Paginator<BicycleDetailsDto>.Create(bicyclesDto, request.Pagination);
            return pagination;
        }
    }
}
