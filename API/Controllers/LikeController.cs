using API.Contracts;
using Application.Features.Matching.LikeUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/likes")]
    public class LikesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LikesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Like([FromBody]LikeUserRequest likeUserRequest, CancellationToken ct)
        {
            var command = new LikeUserCommand(likeUserRequest.FromUserId, likeUserRequest.ToUserId);

            var matchResult = await _mediator.Send(command, ct);

            return Ok(new { isMatch = matchResult });
        }
    }
}
