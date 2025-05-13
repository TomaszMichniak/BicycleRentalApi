using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Reservation;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Reservation.Command.Create
{
    public class CreateReservationCommand : IRequest<ReservationDetailsDto>
    {
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid GuestId { get; set; }
        public Guid AddressId { get; set; }
    }
}
