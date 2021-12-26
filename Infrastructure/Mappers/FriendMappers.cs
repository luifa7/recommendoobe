using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public class FriendMappers
    {

        public static Friends FromDomainObjectToDBEntity(Friend friend)
        {
            return new Friends()
            {
                UserDId = friend.UserDId,
                FriendDId = friend.FriendDId,
        };

        }

        public static Friend FromDBEntityToDomainObject(Friends friendDBEntity)
        {
            return new Friend(
                userDId: friendDBEntity.UserDId,
                friendDId: friendDBEntity.FriendDId
                );
        }
    }
}
