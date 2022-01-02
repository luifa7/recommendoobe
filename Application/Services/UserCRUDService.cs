using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class UserCRUDService
    {
        private readonly IUserRepository _userRepository;

        public UserCRUDService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetByDId(string dId)
        {
            return _userRepository.GetByDId(dId);
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
    }
}
