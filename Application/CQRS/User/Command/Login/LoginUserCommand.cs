using MediatR;

namespace Application.CQRS.User.Command.Login
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

    }
}
