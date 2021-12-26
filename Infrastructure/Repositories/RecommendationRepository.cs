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

        public Recommendation GetByDId(string dId)
        {
            Recommendations recommendationFromDB =
                _dbContext.Recommendations.FirstOrDefault(r => r.DId == dId);

            return RecommendationMappers.FromDBEntityToDomainObject(
                recommendationFromDB);
        }

        public List<Recommendation> GetRecommendationsByDIdList(string[] dIds)
        {
            var recommendationsFromDB = _dbContext.Recommendations.Where(r => dIds.Contains(r.DId)).ToList();
            List<Recommendation> recommendations = new();

            recommendationsFromDB.ForEach(re => recommendations.Add
            (RecommendationMappers.FromDBEntityToDomainObject(re)));

            return recommendations;

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
            Cities cityFromDB =
                _dbContext.Cities.FirstOrDefault(
                    c => c.DId == recommendation.CityDId);

            List<Tags> tags =
                _dbContext.Tags.Where(
                    t => recommendation.Tags.Contains(t.Word)).ToList();

            Users fromUserFromDB =
                _dbContext.Users.FirstOrDefault(
                    u => u.DId == recommendation.FromUserDId);

            Users toUserFromDB =
                _dbContext.Users.FirstOrDefault(
                    u => u.DId == recommendation.ToUserDId);

            var recommendationDBEntity =
                RecommendationMappers.FromDomainObjectToDBEntity(
                    recommendation, cityFromDB, tags,
                    fromUserFromDB, toUserFromDB);
            _dbContext.Recommendations.Add(recommendationDBEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
