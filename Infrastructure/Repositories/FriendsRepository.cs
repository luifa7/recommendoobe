using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class FriendRepository: IFriendRepository
    {
        private DBContext _dbContext;

        public FriendRepository()
        {
            _dbContext = new DBContext();
        }

        public List<Friend> GetAllByUserDId(string userDId)
        {
            List<Friends> friendsFromDB =
                _dbContext.Friends.Where(f => f.UserDId == userDId).ToList();

            List<Friend> friends = new();
            friendsFromDB.ForEach(fr => friends.Add
            (FriendMappers.FromDBEntityToDomainObject(fr)));

            return friends;
        }

        public Task PersistAsync(Friend friend)
        {
            var friendDBEntity =
                FriendMappers.FromDomainObjectToDBEntity(friend);
            _dbContext.Friends.Add(friendDBEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
