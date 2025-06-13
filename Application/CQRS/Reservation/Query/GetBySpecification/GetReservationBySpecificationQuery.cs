using Application.DTO.Reservation;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specification;
using MediatR;

namespace Application.CQRS.Reservation.Query.GetBySpecification
{
    public class GetReservationBySpecificationQuery : IRequest<IPagedResult<ReservationDetailsDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public OrderBy OrderBy { get; set; } = 0;
        public Guid? Id { get; set; }
        public decimal? TotalPrice { get; set; }
        public ReservationStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? GuestId { get; set; }
        public Guid? AddressId { get; set; }
    }
}
