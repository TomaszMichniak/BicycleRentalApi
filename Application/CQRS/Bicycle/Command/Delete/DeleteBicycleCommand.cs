using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.GenericHandlers;
using MediatR;

namespace Application.CQRS.Bicycle.Command.Delete
{
    public class DeleteBicycleCommand : IRequest,IHasId
    {
        public Guid Id { get; set; }
        public DeleteBicycleCommand(Guid id)
        {
            Id = id;
        }
    }
}
