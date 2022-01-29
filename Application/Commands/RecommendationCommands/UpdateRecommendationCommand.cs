using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class UpdateRecommendationCommand : IRequest<bool>
    {
        public string DId;
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
        public string[] Tags;

        public UpdateRecommendationCommand(string dId, string placeName,
            string title, string text, string address, string maps,
            string website, string instagram, string facebook,
            string otherLink, string photo, string[] tags)
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
        private readonly RecommendationCRUDService _recommendationService;

        public UpdateRecommendationCommandHandler(
            RecommendationCRUDService recommendationCRUDService)
        {
            _recommendationService = recommendationCRUDService;
        }

        public async Task<bool> Handle(UpdateRecommendationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _recommendationService.UpdateRecommendation(
                    request.DId, request.PlaceName,
                request.Title, request.Text, request.Address, request.Maps,
                request.Website, request.Instagram, request.Facebook,
                request.OtherLink, request.Photo, request.Tags);

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
