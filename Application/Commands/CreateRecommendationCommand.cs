using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreateRecommendationCommand : IRequest<Recommendation>
    {
        public string Title;
        public string Text;
        public string MapLink;
        public string Website;
        public string Photo;

        public CreateRecommendationCommand(string title, string text, string mapLink,
            string website, string photo)
        {
            Title = title;
            Text = text;
            MapLink = mapLink;
            Website = website;
            Photo = photo;
        }
    }

    public class CreateRecommendationCommandHandler : IRequestHandler<CreateRecommendationCommand, Recommendation>
    {
        private readonly RecommendationCRUDService _recommendationService;

        public CreateRecommendationCommandHandler(RecommendationCRUDService recommendationCRUDService)
        {
            _recommendationService = recommendationCRUDService;
        }

        public async Task<Recommendation> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
        {
            var recommendation = Recommendation.Create(request.Title, request.Text,
                request.MapLink, request.Website, request.Photo);

            await _recommendationService.PersistAsync(recommendation);

            return recommendation;
        }
    }
}
