using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FluentValidation;

namespace Application.CQRS.Reservation.Command.CreateReservationWithTransaction
{
    public class CreateReservationWithTransactionCommandValidator : AbstractValidator<CreateReservationWithTransactionCommand>
    {
        public CreateReservationWithTransactionCommandValidator()
        {

            RuleFor(x => x.TotalPrice).GreaterThan(0);
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
            RuleFor(x => x.StartDate).GreaterThan(DateTime.UtcNow.Date);

            RuleFor(x => x.Address).NotNull();
            RuleFor(x => x.Address.City).NotEmpty();
            RuleFor(x => x.Address.Street).NotEmpty();
            RuleFor(x => x.Address.PostalCode).NotEmpty().Matches(@"^\d{2}-\d{3}$");

            RuleFor(x => x.Guest).NotNull();
            RuleFor(x => x.Guest.FirstName).NotEmpty();
            RuleFor(x => x.Guest.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Bicycles).NotEmpty();
            RuleForEach(x => x.Bicycles).ChildRules(bike =>
            {
                bike.RuleFor(x => x.Name).NotEmpty();
                bike.RuleFor(x => x.Size).NotNull();
                bike.RuleFor(x => x.Quantity).GreaterThan(0);
            });
        }
    }
}
