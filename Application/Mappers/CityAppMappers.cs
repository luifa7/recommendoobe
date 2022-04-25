using Domain.Objects;
using DTOs.Cities;

namespace Application.Mappers
{
    public static class CityAppMappers
    {
        public static ReadCity FromDomainObjectToApiDto(City city)
        {
            return new ReadCity(
                dId: city.DId,
                name: city.Name,
                country: city.Country,
                photo: city.Photo,
                userDId: city.UserDId,
                visited: city.Visited
                );
        }
    }
}
