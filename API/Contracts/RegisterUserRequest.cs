using Domain.Enums;

namespace API.Controllers
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
