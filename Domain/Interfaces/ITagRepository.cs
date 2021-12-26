using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface ITagRepository
    {
        /// <summary>
        /// Return a Tag from Database by its Word
        /// </summary>
        /// <param name="word">Word of the Tag to find</param>
        /// <returns></returns>
        Tag GetByWord(string word);

        /// <summary>
        /// Return a Tag from Database by its Word
        /// </summary>
        /// <param name="recommendationDId">RecommendationDId</param>
        /// <param name="word">Word of the Tag to find</param>
        /// <returns></returns>
        Tag GetByWordAndRecommendationDId(string recommendationDId, string word);

        /// <summary>
        /// Return all Tags from Database with tehir words on the list
        /// </summary>
        /// <param name="words">List of Tag Words</param>
        /// <returns></returns>
        List<Tag> GetTagsByWordList(string[] words);

        /// <summary>
        /// Return all Tags from Database by RecommendationDId
        /// </summary>
        /// <param name="recommendationDId">RecommendationDId</param>
        /// <returns></returns>
        List<Tag> GetTagsByRecommendationDId(string recommendationDId);

        /// <summary>
        /// Persist Tag into the Database
        /// </summary>
        /// <param name="tag">Tag to persist</param>
        /// <returns></returns>
        Task PersistAsync(Tag tag);

        /// <summary>
        /// Return all Tags from Database
        /// </summary>
        /// <returns></returns>
        List<Tag> GetAll();
    }
}
