using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ILikeRepository
    {
        public Task<Like?> FindLikeAsync(Guid fromUserId, Guid toUserId, CancellationToken ct);
        public Task AddAsync(Like like, CancellationToken ct);
    }
}
