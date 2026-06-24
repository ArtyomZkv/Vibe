using Application.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Users.UpdateUserProfile
{
    public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserProfileCommand request, CancellationToken ct)
        {
            var currentUser = await _userRepository.GetByIdAsync(request.UserId, ct)
                ?? throw new NotFoundException("Пользователь не найден");

            currentUser.UpdateProfile(request.Name, request.Gender, request.RelationShip, request.DateOfBirth, request.Description, request.City);

            await _userRepository.SaveNewProfileAsync(currentUser, ct);

            return Unit.Value;
        }
    }
}
