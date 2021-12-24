using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface IRecommendationRepository
    {
        /// <summary>
        /// Persist Recommendation into the Database
        /// </summary>
        /// <param name="recommendation">Recommendation to persist</param>
        /// <returns></returns>
        Task PersistAsync(Recommendation recommendation);

        /// <summary>
        /// Return all Recommendations from Database
        /// </summary>
        /// <returns></returns>
        List<Recommendation> GetAll();
    }
}
