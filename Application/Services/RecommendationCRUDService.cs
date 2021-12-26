using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class RecommendationCRUDService
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationCRUDService(IRecommendationRepository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }

        public Recommendation GetByDId(string dId)
        {
            return _recommendationRepository.GetByDId(dId);
        }

        public List<Recommendation> GetRecommendationsByDIdList(string[] dIds)
        {
            return _recommendationRepository.GetRecommendationsByDIdList(dIds);
        }

        public Task PersistAsync(Recommendation recommendation)
        {
            return _recommendationRepository.PersistAsync(recommendation);
        }

        public List<Recommendation> GetAll()
        {
            return _recommendationRepository.GetAll();
        }
    }
}
