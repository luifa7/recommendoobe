using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Objects;
using Infrastructure.Core.Database;
using Infrastructure.Core.Database.Entities;
using Infrastructure.Core.Mappers;
using Microsoft.EntityFrameworkCore;
using DbContext = Infrastructure.Core.Database.DbContext;

namespace Infrastructure.Core.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;

        public RecommendationRepository(ITagRepository tagRepository, IMapper mapper)
        {
            _dbContext = new DbContext();
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public Recommendation GetByDId(string dId)
        {
            Recommendations recommendationFromDb =
                _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .FirstOrDefault(r => r.DId == dId);

            return recommendationFromDb == null ? null : _mapper.Map<Recommendation>(recommendationFromDb);
        }

        public List<Recommendation> GetRecommendationsByDIdList(string[] dIds)
        {
            var recommendationsFromDb = _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .Where(r => dIds.Contains(r.DId)).ToList();
            List<Recommendation> recommendations = new();

            recommendationsFromDb.ForEach(re => recommendations.Add(
                _mapper.Map<Recommendation>(re)));

            return recommendations;
        }

        public List<Recommendation> GetRecommendationsByCityDId(string dId)
        {
            var recommendationsFromDb =
                _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .Where(r => r.City.DId == dId)
                .ToList();

            List<Recommendation> recommendations = new();

            recommendationsFromDb.ForEach(re => recommendations.Add(
                _mapper.Map<Recommendation>(re)));

            return recommendations;
        }

        public List<Recommendation> GetRecommendationsByUserCreatorDId(string dId)
        {
            var recommendationsFromDb =
                _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .Where(r => r.FromUser.DId == dId)
                .ToList();

            List<Recommendation> recommendations = new();

            recommendationsFromDb.ForEach(re => recommendations.Add(
                _mapper.Map<Recommendation>(re)));

            return recommendations;
        }

        public List<Recommendation> GetAll()
        {
            var recommendationsFromDb =
                _dbContext.Recommendations
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .Include(r => r.City)
                .ToList();

            List<Recommendation> recommendations = new();

            recommendationsFromDb.ForEach(re => recommendations.Add(
                _mapper.Map<Recommendation>(re)));

            return recommendations;
        }

        public Task PersistAsync(Recommendation recommendation)
        {
            var cityFromDb =
                _dbContext.Cities.FirstOrDefault(
                    c => c.DId == recommendation.CityDId);

            var fromUserFromDb =
                _dbContext.Users.FirstOrDefault(
                    u => u.DId == recommendation.FromUserDId);

            var toUserFromDb =
                _dbContext.Users.FirstOrDefault(
                    u => u.DId == recommendation.ToUserDId);

            var recommendationDbEntity =
                RecommendationMappers.FromDomainObjectToDbEntity(
                    recommendation,
                    cityFromDb,
                    fromUserFromDb,
                    toUserFromDb);
            _dbContext.Recommendations.Add(recommendationDbEntity);
            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteRecommendation(string dId)
        {
            _dbContext.Remove(
                _dbContext.Recommendations.Single(r => r.DId == dId));
            return _dbContext.SaveChangesAsync();
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
            var recommendation = _dbContext.Recommendations.First(
                r => r.DId == dId);
            var dbTags = _tagRepository.GetTagsByRecommendationDId(dId);
            foreach (var dbTag in dbTags.Where(dbTag => !tags.Contains(dbTag.Word)))
            {
                _tagRepository.DeleteByWordAndRecommendationDId(
                    dId, dbTag.Word);
            }

            foreach (var tag in tags)
            {
                if (dbTags.Where(t => t.Word == tag).ToList().Count >= 1) continue;
                Tag newTag = Tag.Create(dId, tag);
                _tagRepository.PersistAsync(newTag);
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
