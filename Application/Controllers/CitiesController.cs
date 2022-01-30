using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
using DTOs.Cities;
using DTOs.Recommendations;
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
        private readonly TagCRUDService _tagService;
        private readonly RecommendationCRUDService _recommendationService;
        private readonly FriendCRUDService _friendService;

        public CitiesController(IMediator mediator,
            CityCRUDService cityCRUDService,
            TagCRUDService tagCRUDService,
            RecommendationCRUDService recommendationCRUDService,
            FriendCRUDService friendCRUDService)
        {
            _mediator = mediator;
            _cityService = cityCRUDService;
            _tagService = tagCRUDService;
            _recommendationService = recommendationCRUDService;
            _friendService = friendCRUDService;
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

        [HttpGet("{dId}/recommendations")]
        public IActionResult GetRecommendationsByCityDId(string dId)
        {
            var domainRecommendations =
                _recommendationService.GetRecommendationsByCityDId(dId);
            List<ReadRecommendation> recommendations = new();
            foreach (Recommendation domainRecommendation in domainRecommendations)
            {
                List<Tag> domainTags =
                    _tagService.GetTagsByRecommendationDId(domainRecommendation.DId);
                recommendations.Add(
                RecommendationAppMappers.FromDomainObjectToApiDTO(
                    domainRecommendation, domainTags));
            }
            return Ok(recommendations);
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

            var domainFriends = _friendService.GetAllFriendsByUserDId(createCity.UserDId);
            foreach(Friend friend in domainFriends)
            {
                var notificationCommand = new CreateNotificationCommand(
                friend.DId,
                Notification.TypeFriendWillVisitCity,
                city.UserDId
                );
                await _mediator.Send(notificationCommand);
            }
            
            return Created(city.DId, CityAppMappers.FromDomainObjectToApiDTO(city));
        }

        [HttpPut("{dId}")]
        public async Task<IActionResult> Update(string dId,
            [FromBody] UpdateCity updateCity)
        {
            var command = new UpdateCityCommand(
                dId,
                updateCity.Name,
                updateCity.Country,
                updateCity.Photo,
                updateCity.Visited);
            bool success = await _mediator.Send(command);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dId}")]
        public async Task<IActionResult> Delete(string dId)
        {
            var command = new DeleteCityCommand(dId);
            bool success = await _mediator.Send(command);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
