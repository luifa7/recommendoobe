﻿using System.Collections.Generic;
using System.Linq;
using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public class RecommendationMappers
    {
        public static Recommendations FromDomainObjectToDBEntity(
            Recommendation recommendation, List<Tags> tags)
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
                CityDId = recommendation.CityDId,
                Tags = tags,
                FromUserDId = recommendation.FromUserDId,
                ToUserDId = recommendation.ToUserDId,
        };

        }

        public static Recommendation FromDBEntityToDomainObject(Recommendations recommendationDBEntity)
        {
            return new Recommendation(
                dId:recommendationDBEntity.DId,
                placeName:recommendationDBEntity.PlaceName,
                title:recommendationDBEntity.Title,
                text:recommendationDBEntity.Text,
                address:recommendationDBEntity.Address,
                maps:recommendationDBEntity.Maps,
                website:recommendationDBEntity.Website,
                instagram:recommendationDBEntity.Instagram,
                facebook:recommendationDBEntity.Facebook,
                otherLink:recommendationDBEntity.OtherLink,
                photo:recommendationDBEntity.Photo,
                createdOn:recommendationDBEntity.CreatedOn,
                cityDId:recommendationDBEntity.CityDId,
                tags: FromListTagsToArrStr(recommendationDBEntity.Tags),
                fromUserDId:recommendationDBEntity.FromUserDId,
                toUserDId:recommendationDBEntity.ToUserDId
                );
        }

        private static string[] FromListTagsToArrStr(List<Tags> tags)
        {
            return tags.Select(tag => tag.Word).ToArray();
        }
    }
}
