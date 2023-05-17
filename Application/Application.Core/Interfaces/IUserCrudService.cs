using Domain.Core.Objects;

namespace Application.Core.Interfaces;

public interface IUserCrudService
{
    User GetByDId(string dId);
    User GetByUserNameCaseInsensitive(string username);
    List<User> GetUsersByDIdList(string[] dIds);
    List<City> GetCitiesByUserDId(string dId);
    Task PersistAsync(User user);
    List<User> GetAll();
    Task UpdateUser(
        string dId,
        string name,
        string shortFact1,
        string shortFact2,
        string shortFact3,
        string aboutMe,
        string interestedIn,
        string photo);
    Task DeleteUser(string dId);
}