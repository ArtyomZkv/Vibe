using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class InMemoryMatchRepository : IMatchRepository
    {
        private readonly List<Match> _matches = new();
        public Task AddAsync(Match match, CancellationToken ct)
        {
            _matches.Add(match);

            return Task.CompletedTask;
        }
    }
}
