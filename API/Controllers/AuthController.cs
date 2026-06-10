using API.Contracts;
using Application.Features.Auth.SendVerificationCode;
using Application.Features.Auth.VerifyCode;
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

        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] SendCodeRequest codeRequest, CancellationToken ct)
        {
            var codeCommand = new SendVerificationCodeCommand(codeRequest.PhoneNumber);

            await _mediator.Send(codeCommand, ct);

            return Ok();
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest verifyCodeRequest, CancellationToken ct)
        {
            var verifyCodeCommand = new VerifyCodeCommand(verifyCodeRequest.PhoneNumber, verifyCodeRequest.Code);

            var result = await _mediator.Send(verifyCodeCommand, ct);

            return Ok(result);
        }
    }
}
