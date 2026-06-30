using MediatR;
namespace Application.Features.Matching.Messages.GetDialogHistory
{
    public record GetDialogHistoryQuery(Guid DialogId, Guid RequesterId) : IRequest<List<MessageDto>>;
}
