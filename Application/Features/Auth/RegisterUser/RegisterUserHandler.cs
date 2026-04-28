using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken ct)
        {
            var existingUser = await _userRepository.GetByPhoneAsync(command.PhoneNumber, ct);

            if (existingUser != null)
                throw new InvalidOperationException("Пользователь c таким номером уже существует");

            var user = new User(command.PhoneNumber, 
                command.Name, command.Gender, command.RelationShip, 
                command.DateOfBirth, command.Description, command.City);

            await _userRepository.AddAsync(user, ct);

            return user.UserId;
        }
    }
}
