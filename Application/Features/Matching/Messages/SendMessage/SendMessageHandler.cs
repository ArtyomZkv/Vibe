using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Matching.Messages.SendMessage
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, Unit>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IDialogRepository _dialogRepository;
        private readonly IMatchRepository _matchRepository;
        public SendMessageHandler(IMessageRepository messageRepository, IDialogRepository dialogRepository, IMatchRepository matchRepository)
        {
            _messageRepository = messageRepository;
            _dialogRepository = dialogRepository;
            _matchRepository = matchRepository;
        }
        public async Task<Unit> Handle(SendMessageCommand command, CancellationToken ct)
        {
            var dialog = await _dialogRepository.GetByIdAsync(command.DialogId, ct) ?? 
                throw new NotFoundException("Указанного диалога не существует");

            var match = await _matchRepository.GetByIdAsync(dialog.MatchId, ct) ??
                throw new NotFoundException("Указанного мэтча не существует");

            if(!match.HasParticipant(command.SenderId))
                throw new ForbiddenException("Вы не участник данного диалога!");

            if (dialog.IsClosed)
                throw new DomainException("Данный диалог уже закрыт");

            var message = new Message(command.DialogId, command.SenderId, command.MessageText);

            await _messageRepository.AddAsync(message, ct);

            return Unit.Value;
        }   

    }
}
