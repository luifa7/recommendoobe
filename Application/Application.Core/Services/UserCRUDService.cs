using Domain.Core.Interfaces;
using Domain.Core.Objects;

namespace Application.Core.Services
{
    public class UserCrudService
    {
        private readonly IUserRepository _userRepository;

        public UserCrudService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetByDId(string dId)
        {
            return _userRepository.GetByDId(dId);
        }

        public User GetByUserNameCaseInsensitive(string username)
        {
            return _userRepository.GetByUserName(username.ToLower());
        }

        public List<User> GetUsersByDIdList(string[] dIds)
        {
            return _userRepository.GetUsersByDIdList(dIds);
        }

        public List<City> GetCitiesByUserDId(string dId)
        {
            return _userRepository.GetCitiesByUserDId(dId);
        }

        public Task PersistAsync(User user)
        {
            return _userRepository.PersistAsync(user);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Task UpdateUser(
            string dId,
            string name,
            string shortFact1,
            string shortFact2,
            string shortFact3,
            string aboutMe,
            string interestedIn,
            string photo)
        {
            return _userRepository.UpdateUser(
                dId,
                name,
                shortFact1,
                shortFact2,
                shortFact3,
                aboutMe,
                interestedIn,
                photo);
        }

        public Task DeleteUser(string dId)
        {
            return _userRepository.DeleteUser(dId);
        }
    }
}
