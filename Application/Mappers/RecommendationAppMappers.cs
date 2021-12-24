using System;
using Domain.Objects;
using DTOs.Recommendations;

namespace Infrastructure.Mappers
{
    public class RecommendationAppMappers
    {
        public static ReadRecommendation FromDomainObjectToApiDTO(Recommendation recommendation)
        {
            return new ReadRecommendation(
                dId: recommendation.DId,
                title: recommendation.Title,
                text: recommendation.Text,
                mapLink: recommendation.MapLink,
                website: recommendation.Website,
                photo: recommendation.Photo
                );

        }
    }
}
