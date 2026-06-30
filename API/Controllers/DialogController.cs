using API.Extensions;
using Application.Features.Matching.Messages.GetDialogHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/dialog")]
    public class DialogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DialogController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpGet("{dialogId:Guid}/history")]
        public async Task<IActionResult> GetDialogHistory(Guid dialogId, CancellationToken ct)
        {
            var query = new GetDialogHistoryQuery(dialogId, User.GetUserId());
            var history = await _mediator.Send(query, ct);
            return Ok(history);
        }
    }
}
