using System;

namespace Domain.Objects
{
    public class User
    {
        public string DId { get; private set; }
        public string UserName { get; private set; }
        public string Name { get; private set; }
        public string ShortFact1 { get; private set; }
        public string ShortFact2 { get; private set; }
        public string ShortFact3 { get; private set; }
        public string AboutMe { get; private set; }
        public string InterestedIn { get; private set; }
        public string Photo { get; private set; }


        public User(string dId, string userName, string name, string shortFact1,
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

        public static User Create(string userName, string name,
            string shortFact1, string shortFact2, string shortFact3,
            string aboutMe, string interestedIn, string photo)
        {
            var DId = Guid.NewGuid().ToString();
            return new User(DId, userName, name, shortFact1, shortFact2,
                shortFact3, aboutMe, interestedIn, photo);
        }
    }
}
