using Application.Features.Matching.Messages.SendMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        
        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task JoinDialog(Guid dialogId) 
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, dialogId.ToString());
        }
        public async Task SendMessage(Guid dialogId, Guid senderId, string messageText) 
        {
            var command = new SendMessageCommand(dialogId, senderId, messageText);
            await _mediator.Send(command);

            await Clients.Group(dialogId.ToString()).SendAsync("ReceiveMessage", dialogId, senderId, messageText);
        }
    }
}
