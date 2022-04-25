using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface IRecommendationRepository
    {
        /// <summary>
        /// Return a Recommendation from Database by its DId.
        /// </summary>
        /// <param name="dId">DId of the Recommendation to find.</param>
        /// <returns>Found recommendation or null.</returns>
        Recommendation GetByDId(string dId);

        /// <summary>
        /// Return all Recommendations from Database with tehir dIds on the list.
        /// </summary>
        /// <param name="dIds">List of Recommendations DIds.</param>
        /// <returns>Found recommendations or empty list.</returns>
        List<Recommendation> GetRecommendationsByDIdList(string[] dIds);

        /// <summary>
        /// Return all Recommendations from Database from city dId.
        /// </summary>
        /// <param name="dId">DId of the City.</param>
        /// <returns>Found recommendations or empty list.</returns>
        List<Recommendation> GetRecommendationsByCityDId(string dId);

        /// <summary>
        /// Return all Recommendations from Database from user creator dId.
        /// </summary>
        /// <param name="dId">DId of the User that created the recommendation.</param>
        /// <returns>Found recommendations or empty list.</returns>
        List<Recommendation> GetRecommendationsByUserCreatorDId(string dId);

        /// <summary>
        /// Persist Recommendation into the Database.
        /// </summary>
        /// <param name="recommendation">Recommendation to persist.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task PersistAsync(Recommendation recommendation);

        /// <summary>
        /// Return all Recommendations from Database.
        /// </summary>
        /// <returns>Found recommendations or empty list.</returns>
        List<Recommendation> GetAll();

        /// <summary>
        /// Delete Recommendation from Database.
        /// </summary>
        /// <param name="dId">DId of the Recommendation to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteRecommendation(string dId);

        /// <summary>
        /// Update Recommendation.
        /// </summary>
        /// <param name="dId">DId of the Recommendation to update.</param>
        /// <param name="placeName">updated placeName for the Recommendation.</param>
        /// <param name="title">updated title for the Recommendation.</param>
        /// <param name="text">updated text for the Recommendation.</param>
        /// <param name="address">updated address for the Recommendation.</param>
        /// <param name="maps">updated maps for the Recommendation.</param>
        /// <param name="website">updated website for the Recommendation.</param>
        /// <param name="instagram">updated instagram for the Recommendation.</param>
        /// <param name="facebook">updated facebook for the Recommendation.</param>
        /// <param name="otherLink">updated otherLink for the Recommendation.</param>
        /// <param name="photo">updated phot for the Recommendation.</param>
        /// <param name="tags">updated tags for the Recommendation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateRecommendation(
            string dId,
            string placeName,
            string title,
            string text,
            string address,
            string maps,
            string website,
            string instagram,
            string facebook,
            string otherLink,
            string photo,
            string[] tags);
    }
}
