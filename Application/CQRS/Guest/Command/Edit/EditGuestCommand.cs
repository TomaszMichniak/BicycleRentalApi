using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Guest;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Guest.Command.Edit
{
    public class EditGuestCommand : GuestDetailsDto,IRequest<GuestDetailsDto>, IHasId
    {
    }
}
