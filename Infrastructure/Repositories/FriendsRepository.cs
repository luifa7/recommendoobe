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
        public const string FriendshipPending = "pending";
        public const string FriendshipAccepted = "accepted";

        public FriendRepository()
        {
            _dbContext = new DBContext();
        }

        public List<Friend> GetAllFriendsByUserDId(string userDId)
        {
            List<Friends> friendsFromDB =
                _dbContext.Friends.Where(
                    f => f.UserDId == userDId
                    && f.Status == FriendshipAccepted
                ).ToList();

            List<Friend> friends = new();
            friendsFromDB.ForEach(fr => friends.Add
            (FriendMappers.FromDBEntityToDomainObject(fr)));

            return friends;
        }

        public List<Friend> GetAllSentPendingByUserDId(string userDId)
        {
            List<Friends> sentFriendsFromDB =
                _dbContext.Friends.Where(
                    f => f.UserDId == userDId
                    && f.Status == FriendshipPending
                ).ToList();

            List<Friend> friends = new();
            sentFriendsFromDB.ForEach(fr => friends.Add
            (FriendMappers.FromDBEntityToDomainObject(fr)));

            return friends;
        }

        public List<Friend> GetAllReceivedPendingByUserDId(string userDId)
        {
            List<Friends> pendingFriendsFromDB =
                _dbContext.Friends.Where(
                    f => f.FriendDId == userDId
                    && f.Status == FriendshipPending
                ).ToList();

            List<Friend> friends = new();
            pendingFriendsFromDB.ForEach(fr => friends.Add
            (FriendMappers.FromDBEntityToDomainObject(fr)));

            return friends;
        }

        public bool IsRequestPendingBetweenUsers(string user1DId, string user2DId)
        {
            List<Friends> pendingFriendsFromDB =
                _dbContext.Friends.Where(
                    f => (
                    (f.UserDId == user1DId && f.FriendDId == user2DId)
                    || (f.UserDId == user2DId && f.FriendDId == user1DId))
                    && f.Status == FriendshipPending
                ).ToList();

            return pendingFriendsFromDB.Count > 0;
        }

        public Task PersistAsync(Friend friend)
        {
            var friendDBEntity =
                FriendMappers.FromDomainObjectToDBEntity(friend);
            _dbContext.Friends.Add(friendDBEntity);
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteFriend(string user1DId, string user2DId)
        {
            _dbContext.Friends.Where(
                f => (
                (f.UserDId == user1DId && f.FriendDId == user2DId)
                || (f.UserDId == user2DId && f.FriendDId == user1DId))
            ).ToList().ForEach(f => _dbContext.Friends.Remove(f));

            return _dbContext.SaveChangesAsync();
        }

        public Task AcceptFriendRequest(string receiverDId, string senderDId,
            Friend friendshipInTheOtherDirection)
        {
            Friends requestFromDB = _dbContext.Friends.FirstOrDefault(
                f => (
                (f.UserDId == senderDId && f.FriendDId == receiverDId))
                && f.Status == FriendshipPending
            );
            if (requestFromDB != null)
            {
                requestFromDB.Status = FriendshipAccepted;
                var friendDBEntity =
                FriendMappers.FromDomainObjectToDBEntity(
                    friendshipInTheOtherDirection);
                _dbContext.Friends.Add(friendDBEntity);
                return _dbContext.SaveChangesAsync();
            }
            else return null;
            
        }
    }
}
