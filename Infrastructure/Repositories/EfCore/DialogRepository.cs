using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories.EfCore
{
    public class DialogRepository : IDialogRepository
    {
        private readonly AppDbContext _dbContext;

        public DialogRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Dialog dialog, CancellationToken ct)
        {
            await _dbContext.Dialogs.AddAsync(dialog, ct);

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
