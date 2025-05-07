using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.GenericHandlers;
using MediatR;

namespace Application.CQRS.Address.Command.Delete
{
    public class DeleteAddressCommand : IRequest, IHasId
    {
        public Guid Id { get; set; }
        public DeleteAddressCommand(Guid id)
        {
            Id = id;
        }
    }
}
