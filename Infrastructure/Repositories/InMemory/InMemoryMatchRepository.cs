using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories.InMemory
{
    public class InMemoryMatchRepository : IMatchRepository
    {
        private readonly List<Match> _matches = new();
        public Task AddAsync(Match match, CancellationToken ct)
        {
            _matches.Add(match);

            return Task.CompletedTask;
        }

        public Task<Match?> GetByIdAsync(Guid matchId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
