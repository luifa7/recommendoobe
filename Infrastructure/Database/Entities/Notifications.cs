using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Entities
{
    [Index(nameof(DId), IsUnique = true)]
    public class Notifications
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DId { get; set; }
        public string UserDId { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public bool WasOpen { get; set; }
        public string RelatedDId { get; set; }
    }
}
