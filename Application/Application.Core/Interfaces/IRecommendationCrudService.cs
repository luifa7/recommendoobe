using Domain.Core.Objects;

namespace Application.Core.Interfaces;

public interface IRecommendationCrudService
{
    Recommendation GetByDId(string dId);
    List<Recommendation> GetRecommendationsByDIdList(string[] dIds);
    List<Recommendation> GetRecommendationsByCityDId(string dId);
    List<Recommendation> GetRecommendationsByUserCreatorDId(string dId);
    Task PersistAsync(Recommendation recommendation);
    List<Recommendation> GetAll();
    Task UpdateRecommendation(
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
        string[] tags);
    Task DeleteRecommendation(string dId);
}