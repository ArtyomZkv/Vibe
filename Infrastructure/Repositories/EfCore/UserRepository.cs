using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.EfCore
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user, CancellationToken ct)
        {
            await _dbContext.Users.AddAsync(user, ct);

            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
        {
            return 
                await _dbContext.Users.FirstOrDefaultAsync(user => user.UserId == userId, ct);
        }

        public async Task<User?> GetByPhoneAsync(string phoneNumber, CancellationToken ct)
        {
            return
                await _dbContext.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber, ct);
        } 
    }
}
