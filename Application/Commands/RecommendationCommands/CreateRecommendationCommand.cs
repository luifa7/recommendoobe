using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreateRecommendationCommand : IRequest<Recommendation>
    {
        public string PlaceName;
        public string Title;
        public string Text;
        public string Address;
        public string Maps;
        public string Website;
        public string Instagram;
        public string Facebook;
        public string OtherLink;
        public string Photo;
        public string CityDId;
        public string[] Tags;
        public string FromUserDId;
        public string ToUserDId;

        public CreateRecommendationCommand(string placeName, string title, string text,
            string address, string maps, string website, string instagram, string facebook,
            string otherLink, string photo, string cityDId, string[] tags,
            string fromUserDId, string toUserDId)
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
        private readonly RecommendationCRUDService _recommendationService;
        private readonly TagCRUDService _tagService;

        public CreateRecommendationCommandHandler(
            RecommendationCRUDService recommendationCRUDService,
            TagCRUDService tagCRUDService)
        {
            _recommendationService = recommendationCRUDService;
            _tagService = tagCRUDService;
        }

        public async Task<Recommendation> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
        {
            var recommendation = Recommendation.Create(request.PlaceName,
                request.Title, request.Text, request.Address, request.Maps,
                request.Website, request.Instagram, request.Facebook,
                request.OtherLink, request.Photo, request.CityDId,
                request.FromUserDId, request.ToUserDId);

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
