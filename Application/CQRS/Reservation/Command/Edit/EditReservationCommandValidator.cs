using FluentValidation;

namespace Application.CQRS.Reservation.Command.Edit
{
    public class EditReservationCommandValidator : AbstractValidator<EditReservationCommand>
    {
        public EditReservationCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull();
            RuleFor(x => x.Guest).NotNull();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
        }
    }
}
