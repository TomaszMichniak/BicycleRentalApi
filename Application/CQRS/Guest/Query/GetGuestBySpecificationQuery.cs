using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Guest;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Guest.Query
{
    public class GetGuestBySpecificationQuery : IRequest<IPagedResult<GuestDetailsDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public OrderBy OrderBy { get; set; } = 0;
        public Guid? Id { get; set; } = default!;
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
    }
}
