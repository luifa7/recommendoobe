using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Return a User from Database by its DId.
        /// </summary>
        /// <param name="dId">DId of the User to find.</param>
        /// <returns>Found user or null.</returns>
        User GetByDId(string dId);

        /// <summary>
        /// Return a User from Database by its UserName.
        /// </summary>
        /// <param name="username">UserName of the User to find.</param>
        /// <returns>Found user or null.</returns>
        User GetByUserName(string username);

        /// <summary>
        /// Return all Users from Database with their dIds on the list.
        /// </summary>
        /// <param name="dIds">List of Users DIds.</param>
        /// <returns>Found users or empty list.</returns>
        List<User> GetUsersByDIdList(string[] dIds);

        /// <summary>
        /// Return all Cities from Database from user dId.
        /// </summary>
        /// <param name="dId">DId of the User.</param>
        /// <returns>Found cities or empty list.</returns>
        List<City> GetCitiesByUserDId(string dId);

        /// <summary>
        /// Persist User into the Database.
        /// </summary>
        /// <param name="user">User to persist.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task PersistAsync(User user);

        /// <summary>
        /// Return all Users from Database.
        /// </summary>
        /// <returns>Found users or empty list.</returns>
        List<User> GetAll();

        /// <summary>
        /// Update User.
        /// </summary>
        /// <param name="dId">DId of the User to update.</param>
        /// <param name="name">updated name for the User.</param>
        /// <param name="shortFact1">updated shortFact1 for the User.</param>
        /// <param name="shortFact2">updated shortFact2 for the User.</param>
        /// <param name="shortFact3">updated shortFact3 for the User.</param>
        /// <param name="aboutMe">updated aboutMe for the User.</param>
        /// <param name="interestedIn">updated interestedIn for the User.</param>
        /// <param name="photo">updated photo for the User.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateUser(
            string dId,
            string name,
            string shortFact1,
            string shortFact2,
            string shortFact3,
            string aboutMe,
            string interestedIn,
            string photo);

        /// <summary>
        /// Delete User from Database.
        /// </summary>
        /// <param name="dId">DId of the User to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteUser(string dId);
    }
}
