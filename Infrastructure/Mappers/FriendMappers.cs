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
                DId = friend.DId,
                UserDId = friend.UserDId,
                FriendDId = friend.FriendDId,
                Status = friend.Status
        };

        }

        public static Friend FromDBEntityToDomainObject(Friends friendDBEntity)
        {
            return new Friend(
                dId: friendDBEntity.DId,
                userDId: friendDBEntity.UserDId,
                friendDId: friendDBEntity.FriendDId,
                status: friendDBEntity.Status
                );
        }
    }
}
