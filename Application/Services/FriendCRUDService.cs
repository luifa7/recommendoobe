using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Objects;

namespace Application.Services
{
    public class FriendCRUDService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendCRUDService(IFriendRepository friednRepository)
        {
            _friendRepository = friednRepository;
        }

        public List<Friend> GetFriendsByUserDID(string userDId)
        {
            return _friendRepository.GetAllByUserDId(userDId);
        }

        public Task PersistAsync(Friend friend)
        {
            return _friendRepository.PersistAsync(friend);
        }
    }
}
