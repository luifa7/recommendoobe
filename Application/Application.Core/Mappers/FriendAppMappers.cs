using Domain.Core.Objects;
using DTOs.Friend;

namespace Application.Core.Mappers
{
    public static class FriendAppMappers
    {
        public static ReadFriend FromDomainObjectToApiDto(Friend friend)
        {
            return new ReadFriend(
                dId: friend.DId,
                userDId: friend.UserDId,
                friendDId: friend.FriendDId
                );
        }
    }
}
