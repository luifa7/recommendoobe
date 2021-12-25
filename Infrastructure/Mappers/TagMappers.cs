using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public class TagMappers
    {
        public static Tags FromDomainObjectToDBEntity(Tag tag)
        {
            return new Tags() { Word = tag.Word };

        }

        public static Tag FromDBEntityToDomainObject(Tags tagDBEntity)
        {
            return new Tag(word: tagDBEntity.Word);
        }
    }
}
