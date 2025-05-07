using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.CQRS.Bicycle.Command.Edit
{
    public class EditBicycleCommandValidator : AbstractValidator<EditBicycleCommand>
    {
        public EditBicycleCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
            RuleFor(x => x.Description).NotNull().MinimumLength(5);
            RuleFor(x => x.Size).NotNull().IsInEnum();
            RuleFor(x => x.ImageUrl).NotNull().MinimumLength(5);
            RuleFor(x => x.PricePerDay).NotNull().GreaterThan(1);
        }
    }
}
