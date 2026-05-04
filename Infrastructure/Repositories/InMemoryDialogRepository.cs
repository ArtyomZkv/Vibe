using Application.Interfaces;
using Domain.Entities;
namespace Infrastructure.Repositories
{
    public class InMemoryDialogRepository : IDialogRepository
    {
        private readonly List<Dialog> _dialogs = new();
        public Task AddAsync(Dialog dialog, CancellationToken ct)
        {
            _dialogs.Add(dialog);

            return Task.CompletedTask;
        }
    }
}
