using Domain.Entities;


namespace Application.Interfaces
{
    public interface IMatchRepository
    {
        public Task AddAsync(Match match, CancellationToken ct);
        public Task<Match?> GetByIdAsync(Guid matchId, CancellationToken ct);
    }
}
