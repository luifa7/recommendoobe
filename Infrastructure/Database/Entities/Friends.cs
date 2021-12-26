using System;
namespace Infrastructure.Database.Entities
{
    public class Friends
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserDId { get; set; }
        public string FriendDId { get; set; }
    }
}
