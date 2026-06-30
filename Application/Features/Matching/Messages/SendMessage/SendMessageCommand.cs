using MediatR;

namespace Application.Features.Matching.Messages.SendMessage
{
    public record SendMessageCommand(Guid DialogId, Guid SenderId, string MessageText) : IRequest<Unit>;
}
