using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.GenericHandlers;
using MediatR;

namespace Application.CQRS.Bicycle.Command.Edit
{
    public class EditBicycleCommand : BicycleDto, IRequest<BicycleDto>,IHasId
    {
    }
}
