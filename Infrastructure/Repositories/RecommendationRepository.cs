using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

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
                _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .FirstOrDefault(r => r.DId == dId);

            if (recommendationFromDB == null) return null;
            return RecommendationMappers.FromDBEntityToDomainObject(
                recommendationFromDB);
        }

        public List<Recommendation> GetRecommendationsByDIdList(string[] dIds)
        {
            var recommendationsFromDB = _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .Where(r => dIds.Contains(r.DId)).ToList();
            List<Recommendation> recommendations = new();

            recommendationsFromDB.ForEach(re => recommendations.Add
            (RecommendationMappers.FromDBEntityToDomainObject(re)));

            return recommendations;

        }

        public List<Recommendation> GetAll()
        {
            List<Recommendations> recommendationsFromDB =
                _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .ToList();

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

            Users fromUserFromDB =
                _dbContext.Users.FirstOrDefault(
                    u => u.DId == recommendation.FromUserDId);

            Users toUserFromDB =
                _dbContext.Users.FirstOrDefault(
                    u => u.DId == recommendation.ToUserDId);

            var recommendationDBEntity =
                RecommendationMappers.FromDomainObjectToDBEntity(
                    recommendation, cityFromDB,
                    fromUserFromDB, toUserFromDB);
            _dbContext.Recommendations.Add(recommendationDBEntity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
