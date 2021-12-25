using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories
{
    public class RecommendationRepository: IRecommendationRepository
    {
        private DBContext _dbContext;

        public RecommendationRepository()
        {
            _dbContext = new DBContext();
        }

        public List<Recommendation> GetAll()
        {
            List<Recommendations> recommendationsFromDB =
                _dbContext.Recommendations.ToList();

            List<Recommendation> recommendations = new();

            recommendationsFromDB.ForEach(re => recommendations.Add
            (RecommendationMappers.FromDBEntityToDomainObject(re)));

            return recommendations;
        }

        public Task PersistAsync(Recommendation recommendation)
        {
            List<Tags> tags =
                _dbContext.Tags.Where(
                    t => recommendation.Tags.Contains(t.Word)).ToList();

            var recommendationDBEntity =
                RecommendationMappers.FromDomainObjectToDBEntity(
                    recommendation, tags);
            _dbContext.Recommendations.Add(recommendationDBEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
