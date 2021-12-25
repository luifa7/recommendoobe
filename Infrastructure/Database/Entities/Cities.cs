using System;

namespace Infrastructure.Database.Entities
{
    public class Cities
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Photo { get; set; }
        public string UserDId { get; set; }
        public bool Visited { get; set; }
    }
}
