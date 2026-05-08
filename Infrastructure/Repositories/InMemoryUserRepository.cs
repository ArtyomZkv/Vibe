using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public Task<User?> GetByPhoneAsync(string phoneNumber, CancellationToken ct)
        {
            var currentUser = _users.FirstOrDefault(user => user.PhoneNumber == phoneNumber);

            return Task.FromResult(currentUser);
        }

        public Task AddAsync(User user, CancellationToken ct)
        {
            _users.Add(user);

            return Task.CompletedTask;
        }

        public Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
        {
            var currentUser = _users.FirstOrDefault(user => user.UserId == userId);

            return Task.FromResult(currentUser);
        }
    }
}
