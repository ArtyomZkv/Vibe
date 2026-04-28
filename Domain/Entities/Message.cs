
namespace Domain.Entities
{
    public class Message
    {
        public Guid MessageId { get; private set; }

        public Guid DialogId { get; private set; }

        public Guid SenderId { get; private set; }

        public string Text { get; private set; }

        public DateTimeOffset SentAt { get; private set; }

        public Message(Guid dialogId, Guid senderId, string text)
        {
            MessageId = Guid.NewGuid();

            DialogId = dialogId;

            SenderId = senderId;

            Text = text;

            SentAt = DateTimeOffset.UtcNow;
        }
    }
}
