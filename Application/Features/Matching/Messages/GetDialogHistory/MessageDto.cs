using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Matching.Messages.GetDialogHistory
{
    public record MessageDto(
        Guid MessageId,
        Guid SenderId,
        string Text,
        DateTimeOffset SentAt);
}
