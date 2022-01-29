using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface IFriendRepository
    {
        /// <summary>
        /// Return a Friend relation from Database by User DId
        /// </summary>
        /// <param name="userDId">DId of the User to find friends status accepted</param>
        /// <returns></returns>
        List<Friend> GetAllFriendsByUserDId(string userDId);

        /// <summary>
        /// Return a list of pending sent Friend relation from Database by User DId
        /// </summary>
        /// <param name="userDId">DId of the User to find friends status pending</param>
        /// <returns></returns>
        List<Friend> GetAllSentPendingByUserDId(string userDId);

        /// <summary>
        /// Return a list of pending received Friend relation from Database by User DId
        /// </summary>
        /// <param name="userDId">DId of the User to find friends status pending</param>
        /// <returns></returns>
        List<Friend> GetAllReceivedPendingByUserDId(string userDId);

        /// <summary>
        /// Return if there is a relation pending between users
        /// </summary>
        /// <param name="user1DId">DId of one User</param>
        /// <param name="user2DId">DId of another User</param>
        /// <returns></returns>
        bool IsRequestPendingBetweenUsers(string user1DId, string user2DId);

        /// <summary>
        /// Persist Friend relation into the Database
        /// </summary>
        /// <param name="friend">Friend relation to persist</param>
        /// <returns></returns>
        Task PersistAsync(Friend friend);

        /// <summary>
        /// Delete pending Friend relation into the Database
        /// </summary>
        /// <param name="user1DId">One user of the relation</param>
        /// <param name="user2DId">Other user of the relation</param>
        /// <returns></returns>
        Task DeleteFriend(string user1DId, string user2DId);

        /// <summary>
        /// Delete all Friend relation from a User into the Database
        /// </summary>
        /// <param name="userDId">User did to delete friends</param>
        /// <returns></returns>
        Task DeleteAllFriendForUser(string userDId);

        /// <summary>
        /// Mark friend request as accepted and create it on the other direction
        /// </summary>
        /// <param name="user1DId">One user of the relation</param>
        /// <param name="user2DId">Other user of the relation</param>
        /// <param name="friendshipInTheOtherDirection">
        /// The friendship relation in the other direction</param>
        /// <returns></returns>
        Task AcceptFriendRequest(string receiverDId, string senderDId,
            Friend friendshipInTheOtherDirection);
    }
}
