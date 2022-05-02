using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Domain.Core.Objects;
using Infrastructure.Core.Database;
using Infrastructure.Core.Database.Entities;
using Infrastructure.Core.Mappers;

namespace Infrastructure.Core.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DbContext _dbContext;

        public TagRepository()
        {
            _dbContext = new DbContext();
        }

        public Tag GetByWord(string word)
        {
            Tags tagFromDb =
                _dbContext.Tags.FirstOrDefault(t => t.Word == word);

            return tagFromDb == null ? null : TagMappers.FromDbEntityToDomainObject(tagFromDb);
        }

        public Tag GetByWordAndRecommendationDId(
            string recommendationDId, string word)
        {
            Tags tagFromDb =
                _dbContext.Tags.FirstOrDefault(
                    t => t.RecommendationDId == recommendationDId
                    && t.Word == word);

            return tagFromDb == null ? null : TagMappers.FromDbEntityToDomainObject(tagFromDb);
        }

        public List<Tag> GetTagsByWordList(string[] words)
        {
            var tagFromDb = _dbContext.Tags.Where(
                t => words.Contains(t.Word)).ToList();
            List<Tag> tags = new();

            tagFromDb.ForEach(re => tags.Add(
            TagMappers.FromDbEntityToDomainObject(re)));

            return tags;
        }

        public List<Tag> GetTagsByRecommendationDId(string recommendationDId)
        {
            var tagFromDb = _dbContext.Tags.Where(
                t => t.RecommendationDId == recommendationDId).ToList();
            List<Tag> tags = new();

            tagFromDb.ForEach(re => tags.Add(
            TagMappers.FromDbEntityToDomainObject(re)));

            return tags;
        }

        public List<Tag> GetAll()
        {
            List<Tags> tagFromDb =
                _dbContext.Tags.ToList();

            List<Tag> tags = new();

            tagFromDb.ForEach(re => tags.Add(
            TagMappers.FromDbEntityToDomainObject(re)));

            return tags;
        }

        public Task PersistAsync(Tag tag)
        {
            var tagFromDb =
                TagMappers.FromDomainObjectToDbEntity(tag);
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
