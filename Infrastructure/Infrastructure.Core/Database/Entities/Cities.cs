using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Core.Database.Entities
{
    [Index(nameof(DId), IsUnique = true)]
    public class Cities
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Photo { get; set; }
        public Users User { get; set; }
        public bool Visited { get; set; }
    }
}
