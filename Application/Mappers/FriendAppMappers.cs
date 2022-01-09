using Domain.Objects;
using DTOs.Friends;

namespace Infrastructure.Mappers
{
    public class FriendAppMappers
    {
        public static ReadFriend FromDomainObjectToApiDTO(Friend friend)
        {
            return new ReadFriend(
                dId: friend.DId,
                userDId: friend.UserDId,
                friendDId: friend.FriendDId
                );
        }
    }
}
