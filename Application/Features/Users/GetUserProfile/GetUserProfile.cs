using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.GetUserProfile
{
    public record UserProfileDto(
        Guid UserId,
        string Name,
        int Age,
        string City,
        string Description,
        List<string> Interests,
        List<string> Photos);
}
