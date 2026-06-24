using Domain.Enums;

namespace API.Contracts
{
    public record UpdateProfileRequest(string Name, Gender Gender, RelationShip RelationShip,
        DateOnly DateOfBirth, string Description, string City); 
}
