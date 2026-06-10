using Application.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Users.GetUserProfile
{
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserProfileHandler(IUserRepository userRepository) => _userRepository = userRepository;
        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken ct)
        {
            var currentUser = await _userRepository.GetByIdAsync(request.UserId, ct);

            if (currentUser == null)
                throw new NotFoundException("Пользователь не найден");

            if (!currentUser.IsProfileComplete)
                throw new NotFoundException("Профиль не заполнен");

            var dto = new UserProfileDto(
                UserId: currentUser.UserId,
                Name: currentUser.Name!,
                Age: currentUser.Age,
                City: currentUser.City!,
                Description: currentUser.Description!,
                Interests: currentUser.Interests,
                Photos: currentUser.Photos
                );

            return dto;
        }
    }
}
