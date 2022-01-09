namespace DTOs.Friends
{
    public class ReadFriend
    {
        public string DId { get; set; }
        public string UserDId { get; set; }
        public string FriendDId { get; set; }

        public ReadFriend(string dId, string userDId, string friendDId)
        {
            DId = dId;
            UserDId = userDId;
            FriendDId = friendDId;
        }
    }
}
