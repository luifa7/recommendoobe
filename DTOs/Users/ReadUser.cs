namespace DTOs.Users
{
    public class ReadUser
    {
        public string DId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ShortFact1 { get; set; }
        public string ShortFact2 { get; set; }
        public string ShortFact3 { get; set; }
        public string AboutMe { get; set; }
        public string InterestedIn { get; set; }
        public string Photo { get; set; }

        public ReadUser(string dId, string userName, string name, string shortFact1,
            string shortFact2, string shortFact3, string aboutMe,
            string interestedIn, string photo)
        {
            DId = dId;
            UserName = userName;
            Name = name;
            ShortFact1 = shortFact1;
            ShortFact2 = shortFact2;
            ShortFact3 = shortFact3;
            AboutMe = aboutMe;
            InterestedIn = interestedIn;
            Photo = photo;
        }
    }
}
