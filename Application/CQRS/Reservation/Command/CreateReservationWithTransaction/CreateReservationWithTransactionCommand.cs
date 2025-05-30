using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Address;
using Application.DTO.Bicycle;
using Application.DTO.Guest;
using Application.DTO.Reservation;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Reservation.Command.CreateReservationWithTransaction
{
   public class CreateReservationWithTransactionCommand : IRequest<ReservationDetailsDto>
    {
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AddressDto Address { get; set; } = default!;
        public GuestCreateDto Guest { get; set; } = default!;
        public IEnumerable<BicycleCartItem> Bicycles { get; set; } = new List<BicycleCartItem>();
    }
}
