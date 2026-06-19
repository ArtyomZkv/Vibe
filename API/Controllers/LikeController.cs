using API.Contracts;
using API.Extensions;
using Application.Features.Matching.LikeUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/likes")]
    public class LikesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LikesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like([FromBody]LikeUserRequest likeUserRequest, CancellationToken ct)
        {
            var command = new LikeUserCommand(User.GetUserId(), likeUserRequest.ToUserId);

            var matchResult = await _mediator.Send(command, ct);

            return Ok(new { isMatch = matchResult });
        }
    }
}
