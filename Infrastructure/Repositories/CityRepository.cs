using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CityRepository: ICityRepository
    {
        private DBContext _dbContext;

        public CityRepository()
        {
            _dbContext = new DBContext();
        }

        public City GetByDId(string dId)
        {
            Cities cityFromDB =
                _dbContext.Cities.Include(c => c.User)
                .FirstOrDefault(c => c.DId == dId);

            if (cityFromDB == null) return null;
            return CityMappers.FromDBEntityToDomainObject(cityFromDB);
        }

        public List<City> GetCitiesByDIdList(string[] dIds)
        {
            var citiesFromDB = _dbContext.Cities.Include(c => c.User)
                .Where(c => dIds.Contains(c.DId)).ToList();
            List<City> cities = new();

            citiesFromDB.ForEach(re => cities.Add
            (CityMappers.FromDBEntityToDomainObject(re)));

            return cities;

        }

        public List<City> GetAll()
        {
            List<Cities> citiesFromDB =
                _dbContext.Cities.Include(u => u.User).ToList();

            List<City> cities = new();

            citiesFromDB.ForEach(re => cities.Add
            (CityMappers.FromDBEntityToDomainObject(re)));

            return cities;
        }

        public Task PersistAsync(City city)
        {
            Users userFromDB =
                _dbContext.Users.FirstOrDefault(u => u.DId == city.UserDId);
            var cityDBEntity =
                CityMappers.FromDomainObjectToDBEntity(city, userFromDB);
            _dbContext.Cities.Add(cityDBEntity);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateCity(string dId, string name, string country,
            string photo, bool visited)
        {
            var city = _dbContext.Cities.First(c => c.DId == dId);
            city.Name = name;
            city.Country = country;
            city.Photo = photo;
            city.Visited = visited;
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteCity(string dId)
        {
            _dbContext.Remove(_dbContext.Cities.Single(c => c.DId == dId));
            return _dbContext.SaveChangesAsync();
        }
    }
}
