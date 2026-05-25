using Domain.Enums;

namespace API.Contracts
{
    public record RegisterUserRequest(
        string PhoneNumber,

        string Name,

        Gender Gender,

        RelationShip RelationShip,

        DateOnly DateOfBirth,

        string Description,

        string City
        );
}
