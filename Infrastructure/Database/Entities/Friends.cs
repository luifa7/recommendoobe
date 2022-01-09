using System;
using Infrastructure.Repositories;

namespace Infrastructure.Database.Entities
{
    public class Friends
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DId { get; set; }
        public string UserDId { get; set; }
        public string FriendDId { get; set; }
        public string Status { get; set; } = FriendRepository.FriendshipPending;
    }
}
