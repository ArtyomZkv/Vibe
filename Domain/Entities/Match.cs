
namespace Domain.Entities
{
    public class Match
    {
        public Guid MatchId { get; private set; }

        public Guid FirstUserId { get; private set; }

        public Guid SecondUserId { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public Match(Guid firstUserId, Guid secondUserId) 
        {
            MatchId = Guid.NewGuid();

            FirstUserId = firstUserId;

            SecondUserId = secondUserId;

            CreatedAt = DateTimeOffset.UtcNow;
        }

    }
}
