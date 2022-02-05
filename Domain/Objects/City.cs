using System;

namespace Domain.Objects
{
    public class City
    {
        public string DId { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string Photo { get; private set; }
        public string UserDId { get; private set; }
        public bool Visited { get; private set; }


        private City(string dId, string name, string country, string photo,
            string userDId, bool visited)
        {
            DId = dId;
            Name = name;
            Country = country;
            Photo = photo;
            UserDId = userDId;
            Visited = visited;
        }

        public static City Create(string name, string country, string photo,
            string userDId, bool visited)
        {
            var DId = Guid.NewGuid().ToString();
            return new City(DId, name, country, photo, userDId, visited);
        }
    }
}

// TODO: private constructor