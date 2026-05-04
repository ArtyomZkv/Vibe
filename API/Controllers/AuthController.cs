using Application.Features.Auth.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("register")]
        public async Task <IActionResult> Register([FromBody] RegisterUserRequest userRequest, CancellationToken ct)
        {
            var userCommand = new RegisterUserCommand(userRequest.PhoneNumber, userRequest.Name, userRequest.Gender,
                userRequest.RelationShip, userRequest.DateOfBirth, userRequest.Description, userRequest.City);

            var userId = await _mediator.Send(userCommand, ct);

            return Ok(userId);
        } 
    }
}
