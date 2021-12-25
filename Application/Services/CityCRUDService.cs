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

        public Task PersistAsync(City city)
        {
            return _cityRepository.PersistAsync(city);
        }

        public List<City> GetAll()
        {
            return _cityRepository.GetAll();
        }
    }
}
