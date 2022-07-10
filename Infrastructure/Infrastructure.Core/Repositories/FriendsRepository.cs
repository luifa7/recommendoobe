using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Objects;
using Infrastructure.Core.Database;
using Infrastructure.Core.Database.Entities;

namespace Infrastructure.Core.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        public const string FriendshipPending = "pending";
        private const string FriendshipAccepted = "accepted";

        public FriendRepository(IMapper mapper)
        {
            _dbContext = new DbContext();
            _mapper = mapper;
        }

        public List<Friend> GetAllFriendsByUserDId(string userDId)
        {
            var friendsFromDb =
                _dbContext.Friends.Where(
                    f => f.UserDId == userDId
                    && f.Status == FriendshipAccepted
                ).ToList();

            List<Friend> friends = new();
            friendsFromDb.ForEach(friendFromDb => friends.Add(_mapper.Map<Friend>(friendFromDb)));

            return friends;
        }

        public List<Friend> GetAllSentPendingByUserDId(string userDId)
        {
            var sentFriendsFromDb =
                _dbContext.Friends.Where(
                    f => f.UserDId == userDId
                    && f.Status == FriendshipPending
                ).ToList();

            List<Friend> friends = new();
            sentFriendsFromDb.ForEach(friendFromDb => friends.Add(_mapper.Map<Friend>(friendFromDb)));

            return friends;
        }

        public List<Friend> GetAllReceivedPendingByUserDId(string userDId)
        {
            var pendingFriendsFromDb =
                _dbContext.Friends.Where(
                    f => f.FriendDId == userDId
                    && f.Status == FriendshipPending
                ).ToList();

            List<Friend> friends = new();
            pendingFriendsFromDb.ForEach(friendFromDb => friends.Add(_mapper.Map<Friend>(friendFromDb)));

            return friends;
        }

        public bool IsRequestPendingBetweenUsers(string user1DId, string user2DId)
        {
            var pendingFriendsFromDb =
                _dbContext.Friends.Where(
                    f => (
                    (f.UserDId == user1DId && f.FriendDId == user2DId)
                    || (f.UserDId == user2DId && f.FriendDId == user1DId))
                    && f.Status == FriendshipPending
                ).ToList();

            return pendingFriendsFromDb.Count > 0;
        }

        public Task PersistAsync(Friend friend)
        {
            var friendDbEntity = _mapper.Map<Friends>(friend);
            _dbContext.Friends.Add(friendDbEntity);
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

        public Task DeleteAllFriendForUser(string userDId)
        {
            _dbContext.Friends.Where(
                f => f.UserDId == userDId || f.FriendDId == userDId
            ).ToList().ForEach(f => _dbContext.Friends.Remove(f));

            return _dbContext.SaveChangesAsync();
        }

        public Task AcceptFriendRequest(
            string receiverDId,
            string senderDId,
            Friend friendshipInTheOtherDirection)
        {
            var requestFromDb = _dbContext.Friends.FirstOrDefault(
                f =>
                (f.UserDId == senderDId && f.FriendDId == receiverDId)
                && f.Status == FriendshipPending
            );
            if (requestFromDb == null) return Task.CompletedTask;

            requestFromDb.Status = FriendshipAccepted;
            var friendDbEntity = _mapper.Map<Friends>(friendshipInTheOtherDirection);
            _dbContext.Friends.Add(friendDbEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
