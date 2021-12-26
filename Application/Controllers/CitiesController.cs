using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
using DTOs.Cities;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class CitiesController: Controller
    {
        private readonly IMediator _mediator;
        private readonly CityCRUDService _cityService;

        public CitiesController(IMediator mediator, CityCRUDService cityCRUDService)
        {
            _mediator = mediator;
            _cityService = cityCRUDService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var domainCities = _cityService.GetAll();
            List<ReadCity> cities = new();
            domainCities.ForEach(dre => cities.Add(
                CityAppMappers.FromDomainObjectToApiDTO(dre)));
            return Ok(cities);
        }

        [HttpGet("{dId}")]
        public IActionResult GetByDId(string dId)
        {
            if (dId.Contains(','))
            {
                string[] citiesDIds = dId.Split(',');
                var domainCities = _cityService.GetCitiesByDIdList(citiesDIds);
                List<ReadCity> cities = new();
                domainCities.ForEach(dcity => cities.Add(
                    CityAppMappers.FromDomainObjectToApiDTO(dcity)));
                return Ok(cities);
            }
            else
            {
                var domainCity = _cityService.GetByDId(dId);
                ReadCity city = CityAppMappers.FromDomainObjectToApiDTO(domainCity);
                return Ok(city);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCity createCity)
        {
            var command = new CreateCityCommand(
                createCity.Name,
                createCity.Country,
                createCity.Photo,
                createCity.UserDId,
                createCity.Visited
                );
            City city = await _mediator.Send(command);

            return Created(city.DId, CityAppMappers.FromDomainObjectToApiDTO(city));
        }
    }
}
