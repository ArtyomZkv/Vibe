using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.EfCore
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _dbContext;

        public MessageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Message message, CancellationToken ct)
        {
            await _dbContext.AddAsync(message, ct);

            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Message>>GetByDialogAsync(Guid dialogId, CancellationToken ct)
        {
            return await _dbContext.Messages
                .Where(message => message.DialogId == dialogId)
                .OrderBy(message => message.SentAt).ToListAsync();
        }
    }
}
