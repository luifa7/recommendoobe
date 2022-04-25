using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class RecommendationCrudService
    {
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationCrudService(IRecommendationRepository recommendationRepository)
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

        public List<Recommendation> GetRecommendationsByCityDId(string dId)
        {
            return _recommendationRepository.GetRecommendationsByCityDId(dId);
        }

        public List<Recommendation> GetRecommendationsByUserCreatorDId(string dId)
        {
            return _recommendationRepository.GetRecommendationsByUserCreatorDId(dId);
        }

        public Task PersistAsync(Recommendation recommendation)
        {
            return _recommendationRepository.PersistAsync(recommendation);
        }

        public List<Recommendation> GetAll()
        {
            return _recommendationRepository.GetAll();
        }

        public Task UpdateRecommendation(
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
            return _recommendationRepository.UpdateRecommendation(
                dId,
                placeName,
                title,
                text,
                address,
                maps,
                website,
                instagram,
                facebook,
                otherLink,
                photo,
                tags);
        }

        public Task DeleteRecommendation(string dId)
        {
            return _recommendationRepository.DeleteRecommendation(dId);
        }
    }
}
