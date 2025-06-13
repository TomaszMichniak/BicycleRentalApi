using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IJwtService
    {
        public string GenerateAccessToken(User user);
    }
}
