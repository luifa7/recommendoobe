using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
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

        public static Recommendation FromDbEntityToDomainObject(Recommendations recommendationDbEntity)
        {
            return new Recommendation(
                dId: recommendationDbEntity.DId,
                placeName: recommendationDbEntity.PlaceName,
                title: recommendationDbEntity.Title,
                text: recommendationDbEntity.Text,
                address: recommendationDbEntity.Address,
                maps: recommendationDbEntity.Maps,
                website: recommendationDbEntity.Website,
                instagram: recommendationDbEntity.Instagram,
                facebook: recommendationDbEntity.Facebook,
                otherLink: recommendationDbEntity.OtherLink,
                photo: recommendationDbEntity.Photo,
                createdOn: recommendationDbEntity.CreatedOn,
                cityDId: recommendationDbEntity.City.DId,
                fromUserDId: recommendationDbEntity.FromUser.DId,
                toUserDId: recommendationDbEntity.ToUser.DId
                );
        }
    }
}
