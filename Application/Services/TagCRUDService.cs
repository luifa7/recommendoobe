using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Domain.Core.Objects;

namespace Application.Core.Services
{
    public class TagCrudService
    {
        private readonly ITagRepository _tagRepository;

        public TagCrudService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Tag GetByWord(string word)
        {
            return _tagRepository.GetByWord(word);
        }

        public Tag GetByWordAndRecommendationDId(
            string recommendationDId,
            string word)
        {
            return _tagRepository.GetByWordAndRecommendationDId(
                recommendationDId,
                word);
        }

        public List<Tag> GetTagsByWordList(string[] words)
        {
            return _tagRepository.GetTagsByWordList(words);
        }

        public List<Tag> GetTagsByRecommendationDId(string recommendationDId)
        {
            return _tagRepository.GetTagsByRecommendationDId(recommendationDId);
        }

        public Task PersistAsync(Tag tag)
        {
            return _tagRepository.PersistAsync(tag);
        }

        public List<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }
    }
}
