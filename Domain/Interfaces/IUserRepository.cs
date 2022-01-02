using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Return a User from Database by its DId
        /// </summary>
        /// <param name="dId">DId of the User to find</param>
        /// <returns></returns>
        User GetByDId(String dId);

        /// <summary>
        /// Return all Users from Database with tehir dIds on the list
        /// </summary>
        /// <param name="dIds">List of Users DIds</param>
        /// <returns></returns>
        List<User> GetUsersByDIdList(string[] dIds);

        /// <summary>
        /// Return all Cities from Database from user dId
        /// </summary>
        /// <param name="dId">DId of the User</param>
        /// <returns></returns>
        List<City> GetCitiesByUserDId(string dId);

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
