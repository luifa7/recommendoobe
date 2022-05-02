using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Core.Database.Entities
{
    [Index(nameof(RecommendationDId))]
    public class Tags
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string RecommendationDId { get; set; }
        public string Word { get; set; }
    }
}
