using Domain.Objects;
using DTOs.Cities;

namespace Infrastructure.Mappers
{
    public class CityAppMappers
    {
        public static ReadCity FromDomainObjectToApiDTO(City city)
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
