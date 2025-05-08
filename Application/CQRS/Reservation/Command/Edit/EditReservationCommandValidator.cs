using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.CQRS.Reservation.Command.Edit
{
    public class EditReservationCommandValidator: AbstractValidator<EditReservationCommand>
    {
        public EditReservationCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.AddressId).NotNull();
            RuleFor(x => x.GuestId).NotNull();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
        }
    }
}
