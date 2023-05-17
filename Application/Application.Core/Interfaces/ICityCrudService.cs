using Domain.Core.Objects;

namespace Application.Core.Interfaces;

public interface ICityCrudService
{
    City GetByDId(string dId);
    List<City> GetCitiesByDIdList(string[] dIds);
    Task PersistAsync(City city);
    List<City> GetAll();
    Task UpdateCity(string dId, string name, string country, string photo, bool visited);
    Task DeleteCity(string dId);
}