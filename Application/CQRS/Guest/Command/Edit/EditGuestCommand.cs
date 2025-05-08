using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Guest.Command.Edit
{
    public class EditGuestCommand : GuestDto,IRequest<GuestDto>, IHasId
    {
    }
}
