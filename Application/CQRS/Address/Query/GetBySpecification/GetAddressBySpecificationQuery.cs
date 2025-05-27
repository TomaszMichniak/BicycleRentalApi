using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Address.Query.GetBySpecification
{
    public class GetAddressBySpecificationQuery : IRequest<IPagedResult<AddressDetailsDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public OrderBy OrderBy { get; set; } =0;
        public Guid? Id { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }

        public AddressType? addressType { get; set; }
    }
}
