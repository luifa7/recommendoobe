using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private DBContext _dbContext;

        public TagRepository()
        {
            _dbContext = new DBContext();
        }

        public Tag GetByWord(string word)
        {
            Tags tagFromDB =
                _dbContext.Tags.FirstOrDefault(t => t.Word == word);

            return TagMappers.FromDBEntityToDomainObject(tagFromDB);
        }

        public List<Tag> GetTagsByWordList(string[] words)
        {
            var tagsFromDB = _dbContext.Tags.Where(
                t => words.Contains(t.Word)).ToList();
            List<Tag> tags = new();

            tagsFromDB.ForEach(re => tags.Add
            (TagMappers.FromDBEntityToDomainObject(re)));

            return tags;

        }

        public List<Tag> GetAll()
        {
            List<Tags> tagsFromDB =
                _dbContext.Tags.ToList();

            List<Tag> tags = new();

            tagsFromDB.ForEach(re => tags.Add
            (TagMappers.FromDBEntityToDomainObject(re)));

            return tags;
        }

        public Task PersistAsync(Tag tag)
        {
            var tagDBEntity =
                TagMappers.FromDomainObjectToDBEntity(tag);
            _dbContext.Tags.Add(tagDBEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
