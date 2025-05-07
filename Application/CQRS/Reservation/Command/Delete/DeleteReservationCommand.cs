using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.GenericHandlers;
using MediatR;

namespace Application.CQRS.Reservation.Command.Delete
{
    public class DeleteReservationCommand: IRequest, IHasId
    {
        public Guid Id { get; set; }
        public DeleteReservationCommand(Guid id)
        {
            Id = id;
        }
    }
   
}
