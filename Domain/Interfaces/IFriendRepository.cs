using System;
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
        /// <param name="userDId">DId of the User to find friends</param>
        /// <returns></returns>
        List<Friend> GetAllByUserDId(String userDId);

        /// <summary>
        /// Persist Friend relation into the Database
        /// </summary>
        /// <param name="friend">Friend relation to persist</param>
        /// <returns></returns>
        Task PersistAsync(Friend friend);
    }
}
