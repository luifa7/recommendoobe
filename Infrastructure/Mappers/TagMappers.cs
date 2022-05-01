using Domain.Core.Objects;
using Infrastructure.Core.Database.Entities;

namespace Infrastructure.Core.Mappers
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
