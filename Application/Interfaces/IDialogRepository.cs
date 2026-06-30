using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDialogRepository
    {
        Task AddAsync(Dialog dialog, CancellationToken ct);
        Task<Dialog?> GetByIdAsync(Guid dialogGuid, CancellationToken ct);
    }
}