using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Guest;
using MediatR;

namespace Application.CQRS.Guest.Command.Create
{
    public class CreateGuestCommand :GuestCreateDto, IRequest<GuestDetailsDto>
    {

    }
}
