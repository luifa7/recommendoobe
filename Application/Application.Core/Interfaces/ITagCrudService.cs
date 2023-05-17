using Domain.Core.Objects;

namespace Application.Core.Interfaces;

public interface ITagCrudService
{
    Tag GetByWord(string word);
    Tag GetByWordAndRecommendationDId(string recommendationDId, string word);
    List<Tag> GetTagsByWordList(string[] words);
    List<Tag> GetTagsByRecommendationDId(string recommendationDId);
    Task PersistAsync(Tag tag);
    List<Tag> GetAll();
}
