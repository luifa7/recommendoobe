using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface IRecommendationRepository
    {
        /// <summary>
        /// Return a Recommendation from Database by its DId
        /// </summary>
        /// <param name="dId">DId of the Recommendation to find</param>
        /// <returns></returns>
        Recommendation GetByDId(String dId);

        /// <summary>
        /// Return all Recommendations from Database with tehir dIds on the list
        /// </summary>
        /// <param name="dIds">List of Recommendations DIds</param>
        /// <returns></returns>
        List<Recommendation> GetRecommendationsByDIdList(string[] dIds);

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

        /// <summary>
        /// Delete Recommendation from Database
        /// </summary>
        /// <param name="dId">DId of the Recommendation to delete</param>
        /// <returns></returns>
        Task DeleteRecommendation(String dId);
    }
}
