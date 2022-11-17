using Application.Core.Services;
using MediatR;

namespace Application.Core.Commands.RecommendationCommands
{
    public class UpdateRecommendationCommand : IRequest<bool>
    {
        public string DId { get; }
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
        public string[] Tags { get; }

        public UpdateRecommendationCommand(
            string dId,
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
            string[] tags)
        {
            DId = dId;
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
            Tags = tags;
        }
    }

    public class UpdateRecommendationCommandHandler :
        IRequestHandler<UpdateRecommendationCommand, bool>
    {
        private readonly RecommendationCrudService _recommendationService;

        public UpdateRecommendationCommandHandler(
            RecommendationCrudService recommendationCrudService)
        {
            _recommendationService = recommendationCrudService;
        }

        public async Task<bool> Handle(
            UpdateRecommendationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _recommendationService.UpdateRecommendation(
                    request.DId,
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
                    request.Tags);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
        }
    }
}
