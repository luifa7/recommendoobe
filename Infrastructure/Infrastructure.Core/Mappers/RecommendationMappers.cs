using Domain.Core.Objects;
using Infrastructure.Core.Database.Entities;

namespace Infrastructure.Core.Mappers
{
    public static class RecommendationMappers
    {
        public static Recommendations FromDomainObjectToDbEntity(
            Recommendation recommendation,
            Cities city,
            Users fromUser,
            Users toUser)
        {
            return new Recommendations()
            {
                DId = recommendation.DId,
                PlaceName = recommendation.PlaceName,
                Title = recommendation.Title,
                Text = recommendation.Text,
                Address = recommendation.Address,
                Maps = recommendation.Maps,
                Website = recommendation.Website,
                Instagram = recommendation.Instagram,
                Facebook = recommendation.Facebook,
                OtherLink = recommendation.OtherLink,
                Photo = recommendation.Photo,
                CreatedOn = recommendation.CreatedOn,
                City = city,
                FromUser = fromUser,
                ToUser = toUser,
            };
        }
    }
}
