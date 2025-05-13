using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Reservation;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Reservation.Command.CreateReservationWithTransaction
{
   public class CreateReservationWithTransactionCommand : IRequest<ReservationDetailsDto>
    {
        public Domain.Entities.Guest Guest { get; set; } = default!;
    }
}
