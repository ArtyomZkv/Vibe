using MediatR;

namespace Application.Features.Matching.LikeUser
{
    public record LikeUserCommand(Guid FromUserId, Guid ToUserId) : IRequest<bool>;
}
