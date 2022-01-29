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
        private readonly ITagRepository _tagRepository;

        public RecommendationRepository(ITagRepository tagRepository)
        {
            _dbContext = new DBContext();
            _tagRepository = tagRepository;
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

        public List<Recommendation> GetRecommendationsByCityDId(string dId)
        {
            List<Recommendations> recommendationsFromDB =
                _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .Where(r => r.City.DId == dId)
                .ToList();

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

        public Task DeleteRecommendation(string dId)
        {
            _dbContext.Remove(
                _dbContext.Recommendations.Single(r => r.DId == dId));
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateRecommendation(string dId, string placeName,
            string title, string text, string address, string maps,
            string website, string instagram, string facebook, string otherLink,
            string photo, string[] tags)
        {
            var recommendation = _dbContext.Recommendations.First(
                r => r.DId == dId);
            var dbTags = _tagRepository.GetTagsByRecommendationDId(dId);
            foreach(var dbTag in dbTags)
            {
                if (!tags.Contains(dbTag.Word))
                {
                    _tagRepository.DeleteByWordAndRecommendationDId(
                        dId, dbTag.Word);
                }
            }

            foreach(var tag in tags)
            {
                if(dbTags.Where(t => t.Word == tag).ToList().Count < 1){
                    Tag newTag = Tag.Create(dId, tag);
                    _tagRepository.PersistAsync(newTag);
                }
            }

            recommendation.PlaceName = placeName;
            recommendation.Title = title;
            recommendation.Text = text;
            recommendation.Address = address;
            recommendation.Maps = maps;
            recommendation.Website = website;
            recommendation.Instagram = instagram;
            recommendation.Facebook = facebook;
            recommendation.OtherLink = otherLink;
            recommendation.Photo = photo;
            return _dbContext.SaveChangesAsync();
        }
    }
}
