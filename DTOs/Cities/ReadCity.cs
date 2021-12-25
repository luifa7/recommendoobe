namespace DTOs.Cities
{
    public class ReadCity
    {
        public string DId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Photo { get; set; }
        public string UserDId { get; set; }
        public bool Visited { get; set; }

        public ReadCity(string dId, string name, string country, string photo,
            string userDId, bool visited)
        {
            DId = dId;
            Name = name;
            Country = country;
            Photo = photo;
            UserDId = userDId;
            Visited = visited;
        }
    }
}
