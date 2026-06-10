using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Auth.VerifyCode
{
    public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, AuthResultDto>
    {
        private readonly IVerificationCodeStore _codeStore;

        private readonly IUserRepository _userRepository;

        private readonly ITokenService _tokenService;
        public VerifyCodeHandler(IVerificationCodeStore codeStore, IUserRepository userRepository, ITokenService tokenService) 
        {
            _codeStore = codeStore;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<AuthResultDto> Handle(VerifyCodeCommand request, CancellationToken ct)
        {
            if (!await _codeStore.VerifyCode(request.PhoneNumber, request.Code, ct))
                throw new ValidationException("Неверный код");

            await _codeStore.DeleteCode(request.PhoneNumber, ct);

            var user = await _userRepository.GetByPhoneAsync(request.PhoneNumber, ct);

            if (user == null)
            {
                user = new User(request.PhoneNumber);

                await _userRepository.AddAsync(user, ct);
            }

            var token = _tokenService.GenerateAccessToken(user.UserId);

            AuthResultDto authResultDto = new AuthResultDto(token, user.UserId, user.IsProfileComplete);

            return authResultDto;
        }
    }
}
