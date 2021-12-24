using System;
using System.Collections.Generic;
using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public class RecommendationMappers
    {
        public static Recommendations FromDomainObjectToDBEntity(Recommendation recommendation)
        {
            return new Recommendations()
            {
                DId = recommendation.DId,
                Title = recommendation.Title,
                Text = recommendation.Text,
                MapLink = recommendation.MapLink,
                Website = recommendation.Website,
                Photo = recommendation.Photo
            };

        }

        public static Recommendation FromDBEntityToDomainObject(Recommendations recommendationDBEntity)
        {
            return new Recommendation(
                dId:recommendationDBEntity.DId,
                title:recommendationDBEntity.Title,
                text:recommendationDBEntity.Text,
                mapLink:recommendationDBEntity.MapLink,
                website:recommendationDBEntity.Website,
                photo: recommendationDBEntity.Photo
                );
        }
    }
}
