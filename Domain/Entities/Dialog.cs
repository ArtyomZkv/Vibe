
using Domain.Enums;

namespace Domain.Entities
{
    public class Dialog
    {

        public Guid DialogId { get; private set; }
        public Guid MatchId { get; private set; }

        public bool IsClosed { get; private set; }

        public DateTimeOffset CreatedAt {  get; private set; }

        public DialogCloseReason? CloseReason { get; private set; }

        public Dialog(Guid matchId)
        {
            DialogId = Guid.NewGuid();

            MatchId = matchId;

            IsClosed = false;

            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void Close(DialogCloseReason reason)
        {
            if (IsClosed)
                throw new InvalidOperationException("Диалог уже закрыт!");

            IsClosed = true;
            CloseReason = reason;
        }
    }
}
