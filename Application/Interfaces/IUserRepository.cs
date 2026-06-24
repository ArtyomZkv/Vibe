using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByPhoneAsync(string phoneNumber, CancellationToken ct);
        Task AddAsync(User user, CancellationToken ct);

        Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);

        Task SaveNewProfileAsync(User user, CancellationToken ct);
    }
}
