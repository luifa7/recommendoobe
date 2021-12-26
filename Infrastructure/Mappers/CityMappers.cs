using System;
using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public class CityMappers
    {
        public static Cities FromDomainObjectToDBEntity(City city, Users user)
        {
            return new Cities()
            {
                DId = city.DId,
                Name = city.Name,
                Country = city.Country,
                Photo = city.Photo,
                User = user,
                Visited = city.Visited
        };

        }

        public static City FromDBEntityToDomainObject(Cities cityDBEntity)
        {
            return new City(
                dId:cityDBEntity.DId,
                name: cityDBEntity.Name,
                country: cityDBEntity.Country,
                photo:cityDBEntity.Photo,
                userDId: cityDBEntity.User.DId,
                visited: cityDBEntity.Visited
                );
        }
    }
}
