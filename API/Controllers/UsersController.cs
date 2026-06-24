using API.Contracts;
using API.Extensions;
using Application.Features.Users.GetUserProfile;
using Application.Features.Users.UpdateUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken ct)
        {
            var query = new GetUserProfileQuery(userId);
            var profile = await _mediator.Send(query, ct);
            return Ok(profile);
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest updateProfileRequest, CancellationToken ct)
        {
            var query = new UpdateUserProfileCommand(User.GetUserId(), updateProfileRequest.Name, updateProfileRequest.Gender,
                updateProfileRequest.RelationShip, updateProfileRequest.DateOfBirth, updateProfileRequest.Description, updateProfileRequest.City);
            
            await _mediator.Send(query, ct);

            return Ok();
        }
    }
}
