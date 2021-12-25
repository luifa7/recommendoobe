using System;
using System.Collections.Generic;

namespace Infrastructure.Database.Entities
{
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
        public int CreatedOn { get; set; }
        public string CityDId { get; set; }
        public List<Tags> Tags { get; set; }
        public string FromUserDId { get; set; }
        public string ToUserDId { get; set; }
    }
}
