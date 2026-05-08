using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.GetUserProfile
{
    public record GetUserProfileQuery(Guid UserId) : IRequest<UserProfileDto>;
}
