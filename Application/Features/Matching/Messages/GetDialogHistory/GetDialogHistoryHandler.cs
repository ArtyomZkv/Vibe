using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Matching.Messages.GetDialogHistory
{
    public class GetDialogHistoryHandler : IRequestHandler<GetDialogHistoryQuery, List<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IDialogRepository _dialogRepository;
        private readonly IMatchRepository _matchRepository;

        public GetDialogHistoryHandler(IMessageRepository messageRepository, IDialogRepository dialogRepository, IMatchRepository matchRepository)
        {
            _messageRepository = messageRepository;
            _dialogRepository = dialogRepository;
            _matchRepository = matchRepository;
        }
        public async Task<List<MessageDto>> Handle(GetDialogHistoryQuery request, CancellationToken ct)
        {
            var dialog = await _dialogRepository.GetByIdAsync(request.DialogId, ct)
                ?? throw new NotFoundException("Диалог не найден");

            var match = await _matchRepository.GetByIdAsync(dialog.MatchId, ct)
                ?? throw new NotFoundException("Матч не найден");

            if (!match.HasParticipant(request.RequesterId))
                throw new ForbiddenException("Вы не участник данного диалога");

            var messages = await _messageRepository.GetByDialogAsync(request.DialogId, ct);
            
            return messages
                .Select(m => new MessageDto(m.MessageId, m.SenderId, m.Text, m.SentAt))
                .ToList();
        }
    }
}
