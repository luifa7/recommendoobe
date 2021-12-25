namespace DTOs.Users
{
    public class CreateUser
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ShortFact1 { get; set; }
        public string ShortFact2 { get; set; }
        public string ShortFact3 { get; set; }
        public string AboutMe { get; set; }
        public string InterestedIn { get; set; }
        public string Photo { get; set; }
        public string[] Friends { get; set; }
    }
}
