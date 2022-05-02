using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Domain.Core.Objects;
using Infrastructure.Core.Database;
using Infrastructure.Core.Database.Entities;
using Infrastructure.Core.Mappers;
using Microsoft.EntityFrameworkCore;
using DbContext = Infrastructure.Core.Database.DbContext;

namespace Infrastructure.Core.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DbContext _dbContext;

        public CityRepository()
        {
            _dbContext = new DbContext();
        }

        public City GetByDId(string dId)
        {
            var cityFromDb =
                _dbContext.Cities.Include(c => c.User)
                .FirstOrDefault(c => c.DId == dId);

            return cityFromDb == null ? null : CityMappers.FromDbEntityToDomainObject(cityFromDb);
        }

        public List<City> GetCitiesByDIdList(string[] dIds)
        {
            var citiesFromDb = _dbContext.Cities.Include(c => c.User)
                .Where(c => dIds.Contains(c.DId)).ToList();
            List<City> cities = new();

            citiesFromDb.ForEach(re => cities.Add(
            CityMappers.FromDbEntityToDomainObject(re)));

            return cities;
        }

        public List<City> GetAll()
        {
            List<Cities> citiesFromDb =
                _dbContext.Cities.Include(u => u.User).ToList();

            List<City> cities = new();

            citiesFromDb.ForEach(re => cities.Add(
            CityMappers.FromDbEntityToDomainObject(re)));

            return cities;
        }

        public Task PersistAsync(City city)
        {
            var userFromDb =
                _dbContext.Users.FirstOrDefault(u => u.DId == city.UserDId);
            var cityDbEntity =
                CityMappers.FromDomainObjectToDbEntity(city, userFromDb);
            _dbContext.Cities.Add(cityDbEntity);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateCity(
            string dId,
            string name,
            string country,
            string photo,
            bool visited)
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
