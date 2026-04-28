using Application.Interfaces;
using MediatR;

namespace Application.Features.Auth.SendVerificationCode
{
    public class SendVerificationCodeHandler : IRequestHandler<SendVerificationCodeCommand, Unit>
    {
        private readonly ISmsService _sms;

        public SendVerificationCodeHandler(ISmsService sms)
        {
            _sms = sms;
        }
        public async Task<Unit> Handle(SendVerificationCodeCommand request, CancellationToken ct)
        {
            await _sms.SendCodeAsync(request.PhoneNumber, GenerateRandomCode());
            return Unit.Value;
        }
        private string GenerateRandomCode()
        {
            var code = Random.Shared.Next(1000, 9999).ToString();

            return code;
        }
    }
}
