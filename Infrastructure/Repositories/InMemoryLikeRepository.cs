using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class InMemoryLikeRepository : ILikeRepository
    {
        private readonly List<Like> _likes = new(); 
        public Task<Like?> FindLikeAsync(Guid fromUserId, Guid toUserId, CancellationToken ct)
        {
            var currentLike = _likes.FirstOrDefault(like => like.FromUserId == fromUserId && like.ToUserId == toUserId);

            return Task.FromResult(currentLike);
        }
        public Task AddAsync(Like like, CancellationToken ct)
        {
            _likes.Add(like);

            return Task.CompletedTask;
        }
    }
}
