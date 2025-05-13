using Application.DTO.Bicycle;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Bicycle.Query.GetBySpecification
{
    public class GetBicycleBySpecificationQueryHandler : IRequestHandler<GetBicycleBySpecificationQuery, IPagedResult<BicycleDetailsDto>>
    {
        private readonly IGenericSpecificationSearchService<Domain.Entities.Bicycle, BicycleDetailsDto> _searchService;

        public GetBicycleBySpecificationQueryHandler(IGenericSpecificationSearchService<Domain.Entities.Bicycle, BicycleDetailsDto> searchService)
        {
            _searchService = searchService;
        }

        public async Task<IPagedResult<BicycleDetailsDto>> Handle(GetBicycleBySpecificationQuery request, CancellationToken cancellationToken)
        {
            var specification = new Specification<Domain.Entities.Bicycle>();

            if (request.Name != null)
                specification.Criteria.Add(x => x.Name.ToLower().Contains(request.Name.ToLower()));

            if (request.IsAvailable != null)
                specification.Criteria.Add(x => x.IsAvailable == request.IsAvailable);

            if (request.Size != null)
                specification.Criteria.Add(x => x.Size == request.Size);

            if (request.Description != null)
                specification.Criteria.Add(x => x.Description.ToLower().Contains(request.Description.ToLower()));

            return await _searchService.SearchAsync(specification, request.PageNumber, request.PageSize);
        }
    }
}
