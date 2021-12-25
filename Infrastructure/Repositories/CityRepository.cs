using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class CityRepository: ICityRepository
    {
        private DBContext _dbContext;

        public CityRepository()
        {
            _dbContext = new DBContext();
        }

        public List<City> GetAll()
        {
            List<Cities> citiesFromDB =
                _dbContext.Cities.ToList();

            List<City> cities = new();

            citiesFromDB.ForEach(re => cities.Add
            (CityMappers.FromDBEntityToDomainObject(re)));

            return cities;
        }

        public Task PersistAsync(City city)
        {
            var cityDBEntity =
                CityMappers.FromDomainObjectToDBEntity(city);
            _dbContext.Cities.Add(cityDBEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
