using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Entities
{
    [Index(nameof(DId), IsUnique = true)]
    public class Recommendations
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DId { get; set; }
        public string PlaceName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Address { get; set; }
        public string Maps { get; set; }
        public string Website { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string OtherLink { get; set; }
        public string Photo { get; set; }
        public long CreatedOn { get; set; }
        public Cities City { get; set; }
        public Users FromUser { get; set; }
        public Users ToUser { get; set; }
    }
}
