
namespace Application.Features.Auth.VerifyCode
{
    public record AuthResultDto(
        string token,
        Guid userId,
        bool isProfileComplete
        );
}
