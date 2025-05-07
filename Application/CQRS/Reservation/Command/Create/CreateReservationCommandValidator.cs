using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.CQRS.Reservation.Command.Create
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        //ToDo: Add custom validation for StartDate and EndDate
        public CreateReservationCommandValidator() {
            RuleFor(x => x.AddressId).NotNull();
            RuleFor(x => x.GuestId).NotNull();
            RuleFor(x => x.StartDate).NotEmpty();

        }
    }
}
