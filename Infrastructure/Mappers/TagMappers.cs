using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public static class TagMappers
    {
        public static Tags FromDomainObjectToDbEntity(Tag tag)
        {
            return new Tags()
            {
                RecommendationDId = tag.RecommendationDId,
                Word = tag.Word
            };
        }

        public static Tag FromDbEntityToDomainObject(Tags tagDbEntity)
        {
            return new Tag(
                recommendationDId: tagDbEntity.RecommendationDId,
                word: tagDbEntity.Word
                );
        }
    }
}
