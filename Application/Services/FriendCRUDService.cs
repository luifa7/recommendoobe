using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class FriendCRUDService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendCRUDService(IFriendRepository friednRepository)
        {
            _friendRepository = friednRepository;
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

        public Task DeletePendingFriendRequest(
            string user1DId, string user2DId)
        {
            return _friendRepository.DeletePendingFriendRequest(
                user1DId, user2DId);
        }

        public Task AcceptFriendRequest(
            string receiverDId, string senderDId,
            Friend friendshipInTheOtherDirection)
        {
            return _friendRepository.AcceptFriendRequest(
                receiverDId, senderDId, friendshipInTheOtherDirection);
        }
    }
}
