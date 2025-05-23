using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Bicycle;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Bicycle.Command.Create
{
    public class CreateBicycleCommand :BicycleCreateDto, IRequest<BicycleDetailsDto>
    {
    }
}
