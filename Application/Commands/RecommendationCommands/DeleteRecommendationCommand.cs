using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;

namespace Application.Commands.RecommendationCommands
{
    public class DeleteRecommendationCommand : IRequest<bool>
    {
        public string DId { get; }

        public DeleteRecommendationCommand(string dId)
        {
            DId = dId;
        }
    }

    public class DeleteRecommendationCommandHandler :
        IRequestHandler<DeleteRecommendationCommand, bool>
    {
        private readonly RecommendationCrudService _recommendationService;

        public DeleteRecommendationCommandHandler(
            RecommendationCrudService recommendationCrudService)
        {
            _recommendationService = recommendationCrudService;
        }

        public async Task<bool> Handle(
            DeleteRecommendationCommand request,
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
