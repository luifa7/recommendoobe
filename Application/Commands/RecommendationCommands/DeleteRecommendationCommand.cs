using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands
{
    public class DeleteRecommendationCommand : IRequest<bool>
    {
        public string DId;

        public DeleteRecommendationCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteRecommendationCommandHandler :
        IRequestHandler<DeleteRecommendationCommand, bool>
    {
        private readonly RecommendationCRUDService _recommendationService;

        public DeleteRecommendationCommandHandler(
            RecommendationCRUDService recommendationCRUDService)
        {
            _recommendationService = recommendationCRUDService;
        }

        public async Task<bool> Handle(DeleteRecommendationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _recommendationService.DeleteRecommendation(
                    request.DId);

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
