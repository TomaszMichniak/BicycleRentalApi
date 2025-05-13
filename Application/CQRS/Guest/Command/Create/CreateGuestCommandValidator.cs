using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.CQRS.Guest.Command.Create
{
    public class CreateGuestCommandValidator : AbstractValidator<CreateGuestCommand>
    {
        public CreateGuestCommandValidator()
        {
            RuleFor(x => x.Guest.FirstName).NotEmpty();
            RuleFor(x => x.Guest.LastName).NotEmpty();
            RuleFor(x => x.Guest.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Guest.Phone).NotEmpty();
        }
    }
}
