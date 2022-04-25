using System;
using Domain.Objects;
using Infrastructure.Database.Entities;

namespace Infrastructure.Mappers
{
    public static class CityMappers
    {
        public static Cities FromDomainObjectToDbEntity(City city, Users user)
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

        public static City FromDbEntityToDomainObject(Cities cityDbEntity)
        {
            return new City(
                dId: cityDbEntity.DId,
                name: cityDbEntity.Name,
                country: cityDbEntity.Country,
                photo: cityDbEntity.Photo,
                userDId: cityDbEntity.User.DId,
                visited: cityDbEntity.Visited
                );
        }
    }
}
