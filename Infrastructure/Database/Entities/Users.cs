using System;
using System.Collections.Generic;

namespace Infrastructure.Database.Entities
{
    public class Users
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string[] VisitedPlaces { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string[] Others { get; set; }
    }
}
