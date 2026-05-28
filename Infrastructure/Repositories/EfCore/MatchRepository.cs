using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.EfCore
{
    public class MatchRepository : IMatchRepository
    {
        private readonly AppDbContext _dbContext;

        public MatchRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Match match, CancellationToken ct)
        {
            await _dbContext.Matches.AddAsync(match, ct);

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
