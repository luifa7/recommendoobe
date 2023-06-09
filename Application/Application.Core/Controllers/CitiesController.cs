﻿using Application.Core.Commands.CityCommands;
using Application.Core.Commands.NotificationCommands;
using Application.Core.Interfaces;
using Application.Core.Mappers;
using Application.Core.Services;
using AutoMapper;
using CommunityToolkit.Diagnostics;
using Domain.Core.Objects;
using DTOs.Cities;
using DTOs.Recommendations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Core.Controllers
{
    [Route("[controller]")]
    public class CitiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICityCrudService _cityService;
        private readonly ITagCrudService _tagService;
        private readonly IRecommendationCrudService _recommendationService;
        private readonly IFriendCrudService _friendService;

        public CitiesController(
            IMapper mapper,
            IMediator mediator,
            ICityCrudService cityCrudService,
            ITagCrudService tagCrudService,
            IRecommendationCrudService recommendationCrudService,
            IFriendCrudService friendCrudService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _cityService = cityCrudService;
            _tagService = tagCrudService;
            _recommendationService = recommendationCrudService;
            _friendService = friendCrudService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var domainCities = _cityService.GetAll();
            List<ReadCity> cities = new();
            domainCities.ForEach(dCity => cities.Add(
                _mapper.Map<ReadCity>(dCity)));
            return Ok(cities);
        }

        [HttpGet("{dId}")]
        public IActionResult GetByDId(string dId)
        {
            try
            {
                Guard.IsNotNullOrWhiteSpace(dId);
                string[] citiesDIds = dId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Guard.IsNotEmpty(citiesDIds);
                var domainCities = _cityService.GetCitiesByDIdList(citiesDIds);
                List<ReadCity> cities = new();
                domainCities.ForEach(dCity => cities.Add(_mapper.Map<ReadCity>(dCity)));
                if (!domainCities.Any()) return NotFound();
                return domainCities.Count == 1 ? Ok(cities.First()) : Ok(cities);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{dId}/recommendations")]
        public IActionResult GetRecommendationsByCityDId(string dId)
        {
            try
            {
                Guard.IsNotNullOrWhiteSpace(dId);
                var domainRecommendations =
                    _recommendationService.GetRecommendationsByCityDId(dId);
                List<ReadRecommendation> recommendations = (
                        from domainRecommendation in domainRecommendations
                        let domainTags = _tagService.GetTagsByRecommendationDId(domainRecommendation.DId)
                        select RecommendationAppMappers.FromDomainObjectToApiDto(domainRecommendation, domainTags))
                    .ToList();

                return Ok(recommendations);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCity createCity)
        {
            try
            {
                Guard.IsNotNullOrEmpty(createCity.Name);
                Guard.IsNotNullOrEmpty(createCity.Country);
                Guard.IsNotNullOrEmpty(createCity.UserDId);
                Guard.IsNotNullOrWhiteSpace(createCity.Name);
                Guard.IsNotNullOrWhiteSpace(createCity.Country);
                Guard.IsNotNullOrWhiteSpace(createCity.UserDId);

                var command = new CreateCityCommand(
                    createCity.Name,
                    createCity.Country,
                    createCity.Photo,
                    createCity.UserDId,
                    createCity.Visited
                );
                City city = await _mediator.Send(command);

                var domainFriends = _friendService.GetAllFriendsByUserDId(createCity.UserDId);
                foreach (var notificationCommand in domainFriends.Select(friend => new CreateNotificationCommand(
                             friend.DId,
                             Notification.TypeFriendWillVisitCity,
                             city.UserDId
                         )))
                {
                    await _mediator.Send(notificationCommand);
                }

                return Created(city.DId, _mapper.Map<ReadCity>(city));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{dId}")]
        public async Task<IActionResult> Update(
            string dId,
            [FromBody] UpdateCity updateCity)
        {
            try
            {
                Guard.IsNotNullOrWhiteSpace(dId);
                Guard.IsNotNullOrEmpty(updateCity.Name);
                Guard.IsNotNullOrEmpty(updateCity.Country);
                Guard.IsNotNullOrWhiteSpace(updateCity.Name);
                Guard.IsNotNullOrWhiteSpace(updateCity.Country);

                var command = new UpdateCityCommand(
                    dId,
                    updateCity.Name,
                    updateCity.Country,
                    updateCity.Photo,
                    updateCity.Visited);
                bool success = await _mediator.Send(command);
                return success ? NoContent() : BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{dId}")]
        public async Task<IActionResult> Delete(string dId)
        {
            try
            {
                Guard.IsNotNullOrWhiteSpace(dId);
                var command = new DeleteCityCommand(dId);
                bool success = await _mediator.Send(command);
                return success ? NoContent() : BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}