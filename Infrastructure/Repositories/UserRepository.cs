using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BicycleRentalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
