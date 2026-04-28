using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.RegisterUser
{
    public record RegisterUserCommand(string PhoneNumber, 
        string Name, 
        Gender Gender, 
        RelationShip RelationShip,
        DateOnly DateOfBirth, 
        string Description, 
        string City) : IRequest<Guid>;
}
