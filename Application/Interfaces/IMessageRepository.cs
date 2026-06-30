using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IMessageRepository
    {
        public Task AddAsync(Message message, CancellationToken ct);
        public Task<List<Message>> GetByDialogAsync(Guid dialogId, CancellationToken ct);
    }
}
