
namespace Domain.Entities
{
    public class Like
    {
        public Guid LikeId { get; private set; }

        public Guid FromUserId { get; private set; }

        public Guid ToUserId { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public Like(Guid fromUserId, Guid toUserId)
        {
            if (fromUserId == toUserId)
                throw new InvalidOperationException("Нельзя лайкнуть самого себя");

            LikeId = Guid.NewGuid();

            FromUserId = fromUserId;

            ToUserId = toUserId;

            CreatedAt = DateTimeOffset.UtcNow;

        }

    }
}
