using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Reservation.Query.GetBySpecification;
using Application.CQRS.Reservation;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;
using Application.DTO.Guest;

namespace Application.CQRS.Guest.Query
{
    public class GetGuestBySpecificationQueryHandler : IRequestHandler<GetGuestBySpecificationQuery  , IPagedResult<GuestDetailsDto>>
    {
        private readonly IGenericSpecificationSearchService<Domain.Entities.Guest, GuestDetailsDto> _searchService;

        public GetGuestBySpecificationQueryHandler(IGenericSpecificationSearchService<Domain.Entities.Guest, GuestDetailsDto> genericSpecificationSearchService)
        {
            _searchService = genericSpecificationSearchService;
        }

        public async Task<IPagedResult<GuestDetailsDto>> Handle(GetGuestBySpecificationQuery request, CancellationToken cancellationToken)
        {
            var specification = new Specification<Domain.Entities.Guest>();
            if (request.Id != null)
                specification.Criteria.Add(x => x.Id == request.Id);
            if (request.FirstName != null)
                specification.Criteria.Add(x => x.FirstName.ToLower().Contains(request.FirstName.ToLower()));
            if (request.LastName != null)
                specification.Criteria.Add(x => x.LastName.ToLower().Contains(request.LastName.ToLower()));
            if (request.Email != null)
                specification.Criteria.Add(x => x.Email.ToLower() == request.Email.ToLower());
            if (request.Phone != null)
                specification.Criteria.Add(x => x.Phone.ToLower() == request.Phone.ToLower());

            return await _searchService.SearchAsync(specification, request.PageNumber, request.PageSize);
        }
}
}
