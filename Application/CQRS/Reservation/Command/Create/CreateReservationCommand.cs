using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Reservation.Command.Create
{
    public class CreateReservationCommand : IRequest<ReservationDto>
    {
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid GuestId { get; set; }
        public Guid AddressId { get; set; }
    }
}
