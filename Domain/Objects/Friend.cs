using System;

namespace Domain.Objects
{
    public class Friend
    {
        public string UserDId { get; private set; }
        public string FriendDId { get; private set; }


        public Friend(string userDId, string friendDId)
        {
            UserDId = userDId;
            FriendDId = friendDId;
        }

        public static Friend Create(string userDId, string friendDId)
        {
            return new Friend(userDId, friendDId);
        }
    }
}
