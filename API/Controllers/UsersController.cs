using Application.Features.Users.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken ct)
        {
            var query = new GetUserProfileQuery(userId);
            var profile = await _mediator.Send(query, ct);
            return Ok(profile);
        }
    }
}
