using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUserByEmail(string email);
    }
}
