using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.User.Command.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher<Domain.Entities.User> passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new InvalidDataException("Invalid email or password");
            }
            var password = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (password != PasswordVerificationResult.Success)
                throw new InvalidDataException("Invalid email or password");

            var token = _jwtService.GenerateAccessToken(user);
            return token;
        }
    }
}
