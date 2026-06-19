using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId (this ClaimsPrincipal user)
        {
            Claim? claim = user.FindFirst(ClaimTypes.NameIdentifier) 
                ?? throw new UnauthorizedAccessException("Нет доступа");

            Guid userId = Guid.Parse(claim.Value);

            return userId;
        }
    }
}
