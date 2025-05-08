using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.CQRS.Address.Command.Edit
{
    public class EditAddressCommandValidator : AbstractValidator<EditAddressCommand>
    {
        public EditAddressCommandValidator() {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty().Matches(@"^\d{2}-\d{3}$");
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
        }
    }
}
