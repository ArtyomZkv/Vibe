using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.EfCore
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _dbContext;

        public LikeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Like like, CancellationToken ct)
        {
            await _dbContext.Likes.AddAsync(like, ct);

            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<Like?> FindLikeAsync(Guid fromUserId, Guid toUserId, CancellationToken ct)
        {
            return await _dbContext.Likes.FirstOrDefaultAsync(like => 
                like.FromUserId == fromUserId && like.ToUserId == toUserId, ct);
        }
    }
}
