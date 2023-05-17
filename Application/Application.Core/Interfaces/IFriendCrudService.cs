using Domain.Core.Objects;

namespace Application.Core.Interfaces;

public interface IFriendCrudService
{
    List<Friend> GetAllFriendsByUserDId(string userDId);
    List<Friend> GetAllReceivedPendingByUserDId(string userDId);
    List<Friend> GetAllSentPendingByUserDId(string userDId);
    bool IsRequestPendingBetweenUsers(string user1DId, string user2DId);
    Task PersistAsync(Friend friend);
    Task DeleteFriend(string user1DId, string user2DId);
    Task AcceptFriendRequest(string receiverDId, string senderDId, Friend friendshipInTheOtherDirection);
}