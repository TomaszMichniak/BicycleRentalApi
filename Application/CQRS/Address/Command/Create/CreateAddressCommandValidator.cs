using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.CQRS.Address.Command.Create
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Address.PostalCode).NotEmpty().Matches(@"^\d{2}-\d{3}$");
            RuleFor(x => x.Address.City).NotEmpty();
            RuleFor(x=>x.Address.Street).NotEmpty();
        }
    }
}
