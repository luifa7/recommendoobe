using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Persist User into the Database
        /// </summary>
        /// <param name="user">User to persist</param>
        /// <returns></returns>
        Task PersistAsync(User user);

        /// <summary>
        /// Return all Users from Database
        /// </summary>
        /// <returns></returns>
        List<User> GetAll();
    }
}
