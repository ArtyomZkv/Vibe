using Application.Interfaces;
using MediatR;

namespace Application.Features.Auth.SendVerificationCode
{
    public class SendVerificationCodeHandler : IRequestHandler<SendVerificationCodeCommand, Unit>
    {
        private readonly ISmsService _sms;
        private readonly IVerificationCodeStore _codeStore;

        public SendVerificationCodeHandler(ISmsService sms, IVerificationCodeStore codeStore)
        {
            _sms = sms;
            _codeStore = codeStore;
        }
        public async Task<Unit> Handle(SendVerificationCodeCommand request, CancellationToken ct)
        {
            var code = GenerateRandomCode();

            await _codeStore.SaveCode(request.PhoneNumber, code, ct);

            await _sms.SendCodeAsync(request.PhoneNumber, code);

            return Unit.Value;
        }
        private string GenerateRandomCode()
        {
            var code = Random.Shared.Next(1000, 10000).ToString();

            return code;
        }
    }
}
