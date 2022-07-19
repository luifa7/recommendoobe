namespace Domain.Core.Objects
{
    public class Friend
    {
        public string DId { get; private set; }
        public string UserDId { get; private set; }
        public string FriendDId { get; private set; }
        public string Status { get; private set; }

        public Friend(string dId, string userDId, string friendDId, string status)
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
            var dId = Guid.NewGuid().ToString();
            return new Friend(dId, userDId, friendDId, status);
        }
    }
}
