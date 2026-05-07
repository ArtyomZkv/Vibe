
namespace API.Contracts
{
    public record LikeUserRequest(
        Guid FromUserId,
        Guid ToUserId
        );

}
