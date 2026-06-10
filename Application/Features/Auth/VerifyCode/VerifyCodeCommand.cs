using MediatR;

namespace Application.Features.Auth.VerifyCode
{
    public record VerifyCodeCommand(string PhoneNumber, string Code) : IRequest<AuthResultDto>;
}
