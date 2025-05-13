using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Reservation;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Reservation.Query.GetBySpecification
{
    public class GetReservationBySpecificationQueryHandler : IRequestHandler<GetReservationBySpecificationQuery, IPagedResult<ReservationDetailsDto>>
    {
        private readonly IGenericSpecificationSearchService<Domain.Entities.Reservation, ReservationDetailsDto> _searchService;

        public GetReservationBySpecificationQueryHandler(IGenericSpecificationSearchService<Domain.Entities.Reservation, ReservationDetailsDto> genericSpecificationSearchService)
        {
            _searchService = genericSpecificationSearchService;
        }

        public async Task<IPagedResult<ReservationDetailsDto>> Handle(GetReservationBySpecificationQuery request, CancellationToken cancellationToken)
        {
            var specification = new Specification<Domain.Entities.Reservation>();
            if (request.Id != null)
                specification.Criteria.Add(x => x.Id == request.Id);
            if (request.GuestId != null)
                specification.Criteria.Add(x => x.GuestId == request.GuestId);
            if (request.IsConfirmed != null)
                specification.Criteria.Add(x => x.IsConfirmed == request.IsConfirmed);
            if (request.AddressId != null)
                specification.Criteria.Add(x => x.AddressId == request.AddressId);
            if (request.StartDate != null)
                specification.Criteria.Add(x => x.StartDate >= request.StartDate);
            if (request.EndDate != null)
                specification.Criteria.Add(x => x.EndDate <= request.EndDate);
            return await _searchService.SearchAsync(specification, request.PageNumber, request.PageSize);
        }
    }
}
