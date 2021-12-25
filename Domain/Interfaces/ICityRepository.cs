using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface ICityRepository
    {
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
