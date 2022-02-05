using System;

namespace Domain.Objects
{
    public class Friend
    {
        public string DId { get; private set; }
        public string UserDId { get; private set; }
        public string FriendDId { get; private set; }
        public string Status { get; private set; }


        private Friend(string dId, string userDId, string friendDId, string status)
        {
            DId = dId;
            UserDId = userDId;
            FriendDId = friendDId;
            Status = status;
        }

        public static Friend Create(
            string userDId,
            string friendDId,
            string status)
        {
            var DId = Guid.NewGuid().ToString();
            return new Friend(DId, userDId, friendDId, status);
        }
    }
}
