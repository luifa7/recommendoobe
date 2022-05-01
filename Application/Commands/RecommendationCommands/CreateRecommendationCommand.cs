using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using Domain.Core.Objects;
using MediatR;

namespace Application.Core.Commands.RecommendationCommands
{
    public class CreateRecommendationCommand : IRequest<Recommendation>
    {
        public string PlaceName { get; }
        public string Title { get; }
        public string Text { get; }
        public string Address { get; }
        public string Maps { get; }
        public string Website { get; }
        public string Instagram { get; }
        public string Facebook { get; }
        public string OtherLink { get; }
        public string Photo { get; }
        public string CityDId { get; }
        public string[] Tags { get; }
        public string FromUserDId { get; }
        public string ToUserDId { get; }

        public CreateRecommendationCommand(
            string placeName,
            string title,
            string text,
            string address,
            string maps,
            string website,
            string instagram,
            string facebook,
            string otherLink,
            string photo,
            string cityDId,
            string[] tags,
            string fromUserDId,
            string toUserDId)
        {
            PlaceName = placeName;
            Title = title;
            Text = text;
            Address = address;
            Maps = maps;
            Website = website;
            Instagram = instagram;
            Facebook = facebook;
            OtherLink = otherLink;
            Photo = photo;
            CityDId = cityDId;
            Tags = tags;
            FromUserDId = fromUserDId;
            ToUserDId = toUserDId;
        }
    }

    public class CreateRecommendationCommandHandler : IRequestHandler<CreateRecommendationCommand, Recommendation>
    {
        private readonly RecommendationCrudService _recommendationService;
        private readonly TagCrudService _tagService;

        public CreateRecommendationCommandHandler(
            RecommendationCrudService recommendationCrudService,
            TagCrudService tagCrudService)
        {
            _recommendationService = recommendationCrudService;
            _tagService = tagCrudService;
        }

        public async Task<Recommendation> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
        {
            var recommendation = Recommendation.Create(
                request.PlaceName,
                request.Title,
                request.Text,
                request.Address,
                request.Maps,
                request.Website,
                request.Instagram,
                request.Facebook,
                request.OtherLink,
                request.Photo,
                request.CityDId,
                request.FromUserDId,
                request.ToUserDId);

            await _recommendationService.PersistAsync(recommendation);

            foreach (string tag in request.Tags)
            {
                Tag existingTag = _tagService.GetByWordAndRecommendationDId(
                    recommendation.DId, tag);
                if (existingTag == null)
                {
                    Tag newTag = new(
                    recommendationDId: recommendation.DId,
                    word: tag);
                    await _tagService.PersistAsync(newTag);
                }
            }

            return recommendation;
        }
    }
}
