using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Objects;
using Infrastructure.Core.Database;
using Infrastructure.Core.Database.Entities;

namespace Infrastructure.Core.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public TagRepository(IMapper mapper)
        {
            _dbContext = new DbContext();
            _mapper = mapper;
        }

        public Tag GetByWord(string word)
        {
            Tags tagFromDb =
                _dbContext.Tags.FirstOrDefault(t => t.Word == word);

            return tagFromDb == null ? null : _mapper.Map<Tag>(tagFromDb);
        }

        public Tag GetByWordAndRecommendationDId(
            string recommendationDId, string word)
        {
            Tags tagFromDb =
                _dbContext.Tags.FirstOrDefault(
                    t => t.RecommendationDId == recommendationDId
                    && t.Word == word);

            return tagFromDb == null ? null : _mapper.Map<Tag>(tagFromDb);
        }

        public List<Tag> GetTagsByWordList(string[] words)
        {
            var tagFromDb = _dbContext.Tags.Where(
                t => words.Contains(t.Word)).ToList();
            List<Tag> tags = new();

            tagFromDb.ForEach(t => tags.Add(_mapper.Map<Tag>(t)));

            return tags;
        }

        public List<Tag> GetTagsByRecommendationDId(string recommendationDId)
        {
            var tagFromDb = _dbContext.Tags.Where(
                t => t.RecommendationDId == recommendationDId).ToList();
            List<Tag> tags = new();

            tagFromDb.ForEach(t => tags.Add(_mapper.Map<Tag>(t)));

            return tags;
        }

        public List<Tag> GetAll()
        {
            List<Tags> tagFromDb =
                _dbContext.Tags.ToList();

            List<Tag> tags = new();

            tagFromDb.ForEach(t => tags.Add(_mapper.Map<Tag>(t)));

            return tags;
        }

        public Task PersistAsync(Tag tag)
        {
            var tagFromDb = _mapper.Map<Tags>(tag);
            _dbContext.Tags.Add(tagFromDb);
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteByWordAndRecommendationDId(
            string recommendationDId, string word)
        {
            _dbContext.Remove(
                _dbContext.Tags.Single(
                    t => t.RecommendationDId == recommendationDId
                    && t.Word == word));
            return _dbContext.SaveChangesAsync();
        }
    }
}
