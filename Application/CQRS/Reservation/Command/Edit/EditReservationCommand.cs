using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Reservation;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Reservation.Command.Edit
{
    public class EditReservationCommand :ReservationDetailsDto, IRequest<ReservationDetailsDto>, IHasId
    {
    
    }
}
