using Application.Core.Interfaces;
using Domain.Core.Interfaces;
using Domain.Core.Objects;

namespace Application.Core.Services
{
    public class FriendCrudService: IFriendCrudService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendCrudService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public List<Friend> GetAllFriendsByUserDId(string userDId)
        {
            return _friendRepository.GetAllFriendsByUserDId(userDId);
        }

        public List<Friend> GetAllReceivedPendingByUserDId(string userDId)
        {
            return _friendRepository.GetAllReceivedPendingByUserDId(userDId);
        }

        public List<Friend> GetAllSentPendingByUserDId(string userDId)
        {
            return _friendRepository.GetAllSentPendingByUserDId(userDId);
        }

        public bool IsRequestPendingBetweenUsers(
            string user1DId, string user2DId)
        {
            return _friendRepository.IsRequestPendingBetweenUsers(
                user1DId, user2DId);
        }

        public Task PersistAsync(Friend friend)
        {
            return _friendRepository.PersistAsync(friend);
        }

        public Task DeleteFriend(
            string user1DId, string user2DId)
        {
            return _friendRepository.DeleteFriend(
                user1DId, user2DId);
        }

        public Task AcceptFriendRequest(
            string receiverDId,
            string senderDId,
            Friend friendshipInTheOtherDirection)
        {
            return _friendRepository.AcceptFriendRequest(
                receiverDId, senderDId, friendshipInTheOtherDirection);
        }
    }
}
