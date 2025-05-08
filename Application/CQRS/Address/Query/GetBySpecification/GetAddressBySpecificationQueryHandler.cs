using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Address.Query.GetBySpecification
{
    public class GetAddressBySpecificationQueryHandler : IRequestHandler<GetAddressBySpecificationQuery, IPagedResult<AddressDto>>
    {
        private readonly IGenericSpecificationSearchService<Domain.Entities.Address, AddressDto> _searchService;

        public GetAddressBySpecificationQueryHandler(IGenericSpecificationSearchService<Domain.Entities.Address, AddressDto> searchService)
        {
            _searchService = searchService;
        }

        public async Task<IPagedResult<AddressDto>> Handle(GetAddressBySpecificationQuery request, CancellationToken cancellationToken)
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
