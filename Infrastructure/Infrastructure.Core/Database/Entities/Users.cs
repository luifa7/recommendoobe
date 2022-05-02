using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Core.Database.Entities
{
    [Index(nameof(DId), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class Users
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ShortFact1 { get; set; }
        public string ShortFact2 { get; set; }
        public string ShortFact3 { get; set; }
        public string AboutMe { get; set; }
        public string InterestedIn { get; set; }
        public string Photo { get; set; }
    }
}
