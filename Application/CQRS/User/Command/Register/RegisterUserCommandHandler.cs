using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.User.Command.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher<Domain.Entities.User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetUserByEmail(request.Email);
            if (existing != null)
            {
                throw new InvalidDataException("User with this email already exists.");
            }
            var user = new Domain.Entities.User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                RoleId = request.RoleId,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            await _userRepository.CreateAsync(user);
        }
    }
}
