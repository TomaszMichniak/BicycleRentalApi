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

namespace Application.CQRS.Bicycle.Query.AvailableOnDate
{
    public class GetAvailableByDateQueryHandler : IRequestHandler<GetAvailableByDateQuery, IPagedResult<BicycleDetailsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBicycleRepository _bicycleRepository;

        public GetAvailableByDateQueryHandler(IMapper mapper, IBicycleRepository bicycleRepository)
        {
            _mapper = mapper;
            _bicycleRepository = bicycleRepository;
        }


        public async Task<IPagedResult<BicycleDetailsDto>> Handle(GetAvailableByDateQuery request, CancellationToken cancellationToken)
        {
            var bicycles =await _bicycleRepository.GetAvailableBicyclesByDates(request.StartDate, request.EndDate);
            var bicyclesDto = _mapper.Map<IEnumerable<BicycleDetailsDto>>(bicycles);
            var paginationDto = new PaginationDto
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var pagination = Paginator<BicycleDetailsDto>.Create(bicyclesDto, paginationDto);
            return pagination;
        }
    }
}
