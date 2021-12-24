using System;
using System.Collections.Generic;

namespace Infrastructure.Database.Entities
{
    public class Recommendations
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string MapLink { get; set; }
        public string Website { get; set; }
        public string Photo { get; set; }

    }
}
