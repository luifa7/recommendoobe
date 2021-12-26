using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using DTOs.Recommendations;

namespace Infrastructure.Mappers
{
    public class RecommendationAppMappers
    {
        public static ReadRecommendation FromDomainObjectToApiDTO(
            Recommendation recommendation,
            List<Tag> tags
            )
        {
            return new ReadRecommendation(
                dId: recommendation.DId,
                placeName: recommendation.PlaceName,
                title: recommendation.Title,
                text: recommendation.Text,
                address: recommendation.Address,
                maps: recommendation.Maps,
                website: recommendation.Website,
                instagram: recommendation.Instagram,
                facebook: recommendation.Facebook,
                otherLink: recommendation.OtherLink,
                photo: recommendation.Photo,
                createdOn: recommendation.CreatedOn,
                cityDId: recommendation.CityDId,
                tags: FromTagListToArrayString(tags),
                fromUserDId: recommendation.FromUserDId,
                toUserDId: recommendation.ToUserDId
                );
        }

        public static string[] FromTagListToArrayString(List<Tag> tags)
        {
            return tags.Select(t => t.Word).ToArray();
        }
    }
}
