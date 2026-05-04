using Domain.Entities;


namespace Application.Interfaces
{
    public interface IMatchRepository
    {
        public Task AddAsync(Match match, CancellationToken ct);
    }
}
