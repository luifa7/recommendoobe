using System.Collections.Generic;
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

        public RecommendationsController(IMediator mediator, RecommendationCRUDService recommendationCRUDService)
        {
            _mediator = mediator;
            _recommendationService = recommendationCRUDService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var domainRecommendations = _recommendationService.GetAll();
            List<ReadRecommendation> recommendations = new();
            domainRecommendations.ForEach(dre => recommendations.Add(
                RecommendationAppMappers.FromDomainObjectToApiDTO(dre)));
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
                domainRecommendations.ForEach(dre => recommendations.Add(
                    RecommendationAppMappers.FromDomainObjectToApiDTO(dre)));
                return Ok(recommendations);
            }
            else
            {
                var domainRecommendation = _recommendationService.GetByDId(dId);
                ReadRecommendation recommendation =
                    RecommendationAppMappers
                    .FromDomainObjectToApiDTO(domainRecommendation);
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

            return Created(recommendation.DId, RecommendationAppMappers.FromDomainObjectToApiDTO(recommendation));
        }
    }
}
