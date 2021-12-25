using System;
namespace Infrastructure.Database.Entities
{
    public class Tags
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Word { get; set; }
    }
}
