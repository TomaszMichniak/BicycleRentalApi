using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Guest.Command.Delete
{
    public class DeleteGuestCommand : IRequest, IHasId
    {
        public Guid Id { get; set; }

        public DeleteGuestCommand(Guid id)
        {
            Id = id;
        }
    }
}
