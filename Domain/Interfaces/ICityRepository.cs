using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface ICityRepository
    {
        /// <summary>
        /// Return a City from Database by its DId
        /// </summary>
        /// <param name="dId">DId of the City to find</param>
        /// <returns></returns>
        City GetByDId(String dId);

        /// <summary>
        /// Return all Cities from Database with tehir dIds on the list
        /// </summary>
        /// <param name="dIds">List of City DIds</param>
        /// <returns></returns>
        List<City> GetCitiesByDIdList(string[] dIds);

        /// <summary>
        /// Persist City into the Database
        /// </summary>
        /// <param name="city">City to persist</param>
        /// <returns></returns>
        Task PersistAsync(City city);

        /// <summary>
        /// Return all Recommendations from Database
        /// </summary>
        /// <returns></returns>
        List<City> GetAll();
    }
}
