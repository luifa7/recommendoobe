using Domain.Objects;
using DTOs.Friends;

namespace Application.Mappers
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
