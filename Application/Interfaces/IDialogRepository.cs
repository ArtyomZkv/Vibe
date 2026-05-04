using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDialogRepository
    {
        Task AddAsync(Dialog dialog, CancellationToken ct);
    }
}