using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface ITagRepository
    {
        /// <summary>
        /// Return a Tag from Database by its Word.
        /// </summary>
        /// <param name="word">Word of the Tag to find.</param>
        /// <returns>Found tag or null.</returns>
        Tag GetByWord(string word);

        /// <summary>
        /// Return a Tag from Database by its Word.
        /// </summary>
        /// <param name="recommendationDId">RecommendationDId.</param>
        /// <param name="word">Word of the Tag to find.</param>
        /// <returns>Found tag or null.</returns>
        Tag GetByWordAndRecommendationDId(string recommendationDId, string word);

        /// <summary>
        /// Return all Tags from Database with tehir words on the list.
        /// </summary>
        /// <param name="words">List of Tag Words.</param>
        /// <returns>Found tags o empty list.</returns>
        List<Tag> GetTagsByWordList(string[] words);

        /// <summary>
        /// Return all Tags from Database by RecommendationDId.
        /// </summary>
        /// <param name="recommendationDId">RecommendationDId.</param>
        /// <returns>Found tags or empty list.</returns>
        List<Tag> GetTagsByRecommendationDId(string recommendationDId);

        /// <summary>
        /// Persist Tag into the Database.
        /// </summary>
        /// <param name="tag">Tag to persist.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task PersistAsync(Tag tag);

        /// <summary>
        /// Return all Tags from Database.
        /// </summary>
        /// <returns>Found tags or empty list.</returns>
        List<Tag> GetAll();

        /// <summary>
        /// Delete a Tag from Database by its Word.
        /// </summary>
        /// <param name="recommendationDId">RecommendationDId.</param>
        /// <param name="word">Word of the Tag to find.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteByWordAndRecommendationDId(
            string recommendationDId,
            string word);
    }
}
