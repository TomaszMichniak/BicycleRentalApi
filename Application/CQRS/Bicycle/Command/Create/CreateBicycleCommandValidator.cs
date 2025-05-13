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
            RuleFor(x => x.Bicycle.Name).NotNull().MinimumLength(3);
            RuleFor(x => x.Bicycle.Description).NotNull().MinimumLength(5);
            RuleFor(x => x.Bicycle.Size).NotNull().IsInEnum();
            RuleFor(x => x.Bicycle.ImageUrl).NotNull().MinimumLength(5);
            RuleFor(x => x.Bicycle.PricePerDay).NotNull().GreaterThan(1);
        }
    }
}
