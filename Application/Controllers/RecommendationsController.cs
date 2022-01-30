﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Services;
using Domain.Objects;
using DTOs.Recommendations;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("[controller]")]
    public class RecommendationsController: Controller
    {
        private readonly IMediator _mediator;
        private readonly RecommendationCRUDService _recommendationService;
        private readonly TagCRUDService _tagService;

        public RecommendationsController(IMediator mediator,
            RecommendationCRUDService recommendationCRUDService,
            TagCRUDService tagCRUDService)
        {
            _mediator = mediator;
            _recommendationService = recommendationCRUDService;
            _tagService = tagCRUDService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var domainRecommendations = _recommendationService.GetAll();
            List<ReadRecommendation> recommendations = new();
            foreach(Recommendation domainRecommendation in domainRecommendations)
            {
                List<Tag> domainTags =
                    _tagService.GetTagsByRecommendationDId(domainRecommendation.DId);
                recommendations.Add(
                RecommendationAppMappers.FromDomainObjectToApiDTO(
                    domainRecommendation, domainTags));
            }
            
            return Ok(recommendations);
        }

        [HttpGet("{dId}")]
        public IActionResult GetByDId(string dId)
        {
            if (dId.Contains(','))
            {
                string[] recommendationsDIds = dId.Split(',');
                var domainRecommendations = _recommendationService
                    .GetRecommendationsByDIdList(recommendationsDIds);
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
            else
            {
                var domainRecommendation = _recommendationService.GetByDId(dId);
                List<Tag> domainTags =
                        _tagService.GetTagsByRecommendationDId(dId);
                ReadRecommendation recommendation =
                    RecommendationAppMappers
                    .FromDomainObjectToApiDTO(domainRecommendation, domainTags);
                return Ok(recommendation);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecommendation createRecommendation)
        {
            var command = new CreateRecommendationCommand(
                createRecommendation.PlaceName,
                createRecommendation.Title,
                createRecommendation.Text,
                createRecommendation.Address,
                createRecommendation.Maps,
                createRecommendation.Website,
                createRecommendation.Instagram,
                createRecommendation.Facebook,
                createRecommendation.OtherLink,
                createRecommendation.Photo,
                createRecommendation.CityDId,
                createRecommendation.Tags,
                createRecommendation.FromUserDId,
                createRecommendation.ToUserDId
                );
            Recommendation recommendation = await _mediator.Send(command);
            List<Tag> domainTags =
                        _tagService.GetTagsByRecommendationDId(recommendation.DId);

            var notificationCommand = new CreateNotificationCommand(
                createRecommendation.ToUserDId,
                Notification.TypeRecommendationReceived,
                recommendation.DId
                );
            await _mediator.Send(notificationCommand);

            return Created(recommendation.DId,
                RecommendationAppMappers
                .FromDomainObjectToApiDTO(recommendation, domainTags));
        }

        [HttpPost("copy")]
        public async Task<IActionResult> Copy([FromBody] CopyRecommendation copyRecommendation)
        {
            var existingRecommendation = _recommendationService.GetByDId(copyRecommendation.RecommendationDId);
            List<Tag> existingTags =
                        _tagService.GetTagsByRecommendationDId(copyRecommendation.RecommendationDId);

            var command = new CreateRecommendationCommand(
                existingRecommendation.PlaceName,
                existingRecommendation.Title,
                existingRecommendation.Text,
                existingRecommendation.Address,
                existingRecommendation.Maps,
                existingRecommendation.Website,
                existingRecommendation.Instagram,
                existingRecommendation.Facebook,
                existingRecommendation.OtherLink,
                existingRecommendation.Photo,
                copyRecommendation.CityDId,
                RecommendationAppMappers.FromTagListToArrayString(existingTags),
                existingRecommendation.FromUserDId,
                copyRecommendation.ToUserDId
                );
            Recommendation recommendation = await _mediator.Send(command);
            List<Tag> domainTags =
                        _tagService.GetTagsByRecommendationDId(recommendation.DId);

            return Created(recommendation.DId,
                RecommendationAppMappers
                .FromDomainObjectToApiDTO(recommendation, domainTags));
        }

        [HttpPut("{dId}")]
        public async Task<IActionResult> Update(string dId,
            [FromBody] UpdateRecommendation updateRecommendation)
        {
            var command = new UpdateRecommendationCommand(
                dId,
                updateRecommendation.PlaceName,
                updateRecommendation.Title,
                updateRecommendation.Text,
                updateRecommendation.Address,
                updateRecommendation.Maps,
                updateRecommendation.Website,
                updateRecommendation.Instagram,
                updateRecommendation.Facebook,
                updateRecommendation.OtherLink,
                updateRecommendation.Photo,
                updateRecommendation.Tags
                );
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
            var command = new DeleteRecommendationCommand(dId);
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
