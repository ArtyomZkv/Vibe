using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.UpdateUserProfile
{
    public record UpdateUserProfileCommand(Guid UserId, string Name, Gender Gender, RelationShip RelationShip,
        DateOnly DateOfBirth, string Description, string City) : IRequest<Unit>;
}
