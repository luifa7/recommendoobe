using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface ICityRepository
    {
        /// <summary>
        /// Return a City from Database by its DId.
        /// </summary>
        /// <param name="dId">DId of the City to find.</param>
        /// <returns>Found city or null.</returns>
        City GetByDId(string dId);

        /// <summary>
        /// Return all Cities from Database with tehir dIds on the list.
        /// </summary>
        /// <param name="dIds">List of City DIds.</param>
        /// <returns>Found cities or empty list.</returns>
        List<City> GetCitiesByDIdList(string[] dIds);

        /// <summary>
        /// Persist City into the Database.
        /// </summary>
        /// <param name="city">City to persist.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task PersistAsync(City city);

        /// <summary>
        /// Return all Recommendations from Database.
        /// </summary>
        /// <returns>Found cities or empty list.</returns>
        List<City> GetAll();

        /// <summary>
        /// Update City.
        /// </summary>
        /// <param name="dId">DId of the City to update.</param>
        /// <param name="name">updated name for the City.</param>
        /// <param name="country">updated country for the City.</param>
        /// <param name="photo">updated photo for the City.</param>
        /// <param name="visited">updated status for the City.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateCity(
            string dId,
            string name,
            string country,
            string photo,
            bool visited);

        /// <summary>
        /// Delete City from Database.
        /// </summary>
        /// <param name="dId">DId of the City to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteCity(string dId);
    }
}
