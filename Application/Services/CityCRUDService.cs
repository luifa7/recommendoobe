using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class CityCRUDService
    {
        private readonly ICityRepository _cityRepository;

        public CityCRUDService(ICityRepository cityRepository)
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

        public List<Recommendation> GetRecommendationsByCityDId(string dId)
        {
            return _cityRepository.GetRecommendationsByCityDId(dId);
        }

        public Task PersistAsync(City city)
        {
            return _cityRepository.PersistAsync(city);
        }

        public List<City> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public Task DeleteCity(string dId)
        {
            return _cityRepository.DeleteCity(dId);
        }
    }
}
