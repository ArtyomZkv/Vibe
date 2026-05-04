using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Matching.LikeUser
{
    public class LikeUserHandler : IRequestHandler<LikeUserCommand, bool>
    {
        private readonly ILikeRepository _likeRepository;

        private readonly IMatchRepository _matchRepository;

        private readonly IDialogRepository _dialogRepository;

        public LikeUserHandler(ILikeRepository likeRepository, IMatchRepository matchRepository, IDialogRepository dialogRepository)
        {
            _likeRepository = likeRepository;

            _matchRepository = matchRepository;

            _dialogRepository = dialogRepository;
        }

        public async Task<bool> Handle(LikeUserCommand request, CancellationToken ct)
        {
            var currentLike = await _likeRepository.FindLikeAsync(request.FromUserId, request.ToUserId, ct);

            if (currentLike != null)
                throw new InvalidOperationException("Лайк уже создан");

            var like = new Like(request.FromUserId, request.ToUserId);

            await _likeRepository.AddAsync(like, ct);

            var reciprocalLike = await _likeRepository.FindLikeAsync(request.ToUserId, request.FromUserId, ct);

            if (reciprocalLike != null)
            {
                var match = new Match(reciprocalLike.FromUserId, reciprocalLike.ToUserId);

                await _matchRepository.AddAsync(match, ct);

                var dialog = new Dialog(match.MatchId);

                await _dialogRepository.AddAsync(dialog, ct);

                return true;
            }
            
            return false;

        }
    }
}
