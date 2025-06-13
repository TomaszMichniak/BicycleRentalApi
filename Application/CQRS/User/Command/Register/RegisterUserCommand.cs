using MediatR;

namespace Application.CQRS.User.Command.Register
{
    public class RegisterUserCommand : IRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public int RoleId { get; set; }
    }
}
