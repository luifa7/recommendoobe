using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public static class FriendMappers
    {
        public static Friends FromDomainObjectToDbEntity(Friend friend)
        {
            return new Friends()
            {
                DId = friend.DId,
                UserDId = friend.UserDId,
                FriendDId = friend.FriendDId,
                Status = friend.Status
            };
        }

        public static Friend FromDbEntityToDomainObject(Friends friendDbEntity)
        {
            return new Friend(
                dId: friendDbEntity.DId,
                userDId: friendDbEntity.UserDId,
                friendDId: friendDbEntity.FriendDId,
                status: friendDbEntity.Status
                );
        }
    }
}
