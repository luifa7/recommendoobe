using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class CityCrudService
    {
        private readonly ICityRepository _cityRepository;

        public CityCrudService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public City GetByDId(string dId)
        {
            return _cityRepository.GetByDId(dId);
        }

        public List<City> GetCitiesByDIdList(string[] dIds)
        {
            return _cityRepository.GetCitiesByDIdList(dIds);
        }

        public Task PersistAsync(City city)
        {
            return _cityRepository.PersistAsync(city);
        }

        public List<City> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public Task UpdateCity(
            string dId,
            string name,
            string country,
            string photo,
            bool visited)
        {
            return _cityRepository.UpdateCity(
                dId,
                name,
                country,
                photo,
                visited);
        }

        public Task DeleteCity(string dId)
        {
            return _cityRepository.DeleteCity(dId);
        }
    }
}
