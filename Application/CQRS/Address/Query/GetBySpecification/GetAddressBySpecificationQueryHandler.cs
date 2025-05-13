using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Address.Query.GetBySpecification
{
    public class GetAddressBySpecificationQueryHandler : IRequestHandler<GetAddressBySpecificationQuery, IPagedResult<AddressDetailsDto>>
    {
        private readonly IGenericSpecificationSearchService<Domain.Entities.Address, AddressDetailsDto> _searchService;

        public GetAddressBySpecificationQueryHandler(IGenericSpecificationSearchService<Domain.Entities.Address, AddressDetailsDto> searchService)
        {
            _searchService = searchService;
        }

        public async Task<IPagedResult<AddressDetailsDto>> Handle(GetAddressBySpecificationQuery request, CancellationToken cancellationToken)
        {
            var specification = new Specification<Domain.Entities.Address>();

            if (request.Id != null)
                specification.Criteria.Add(x => x.Id==request.Id);

            if (request.City != null)
                specification.Criteria.Add(x => x.City.ToLower().Contains(request.City.ToLower()));

            if (request.PostalCode != null)
                specification.Criteria.Add(x => x.PostalCode == request.PostalCode);

            if (request.Street != null)
                specification.Criteria.Add(x => x.Street.ToLower().Contains(request.Street.ToLower()));

            return await _searchService.SearchAsync(specification, request.PageNumber, request.PageSize);
        }
    }
}
