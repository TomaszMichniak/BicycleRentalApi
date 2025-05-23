using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.CQRS.Bicycle.Command.Create
{
    public class CreateBicycleCommandValidator : AbstractValidator<CreateBicycleCommand>
    {
        public CreateBicycleCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
            RuleFor(x => x.Description).NotNull().MinimumLength(5);
            RuleFor(x => x.Size).NotNull().IsInEnum();
            RuleFor(x => x.ImageUrl).NotNull().MinimumLength(5);
            RuleFor(x => x.PricePerDay).NotNull().GreaterThan(1);
        }
    }
}
