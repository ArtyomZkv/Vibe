using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task<User?> GetByPhoneAsync(string phoneNumber, CancellationToken ct)
        {
            var user 
        }
    }
}
