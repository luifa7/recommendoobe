using System;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    class RecommendationContext: DbContext
    {
        public DbSet<Recommendations> Recommendations { get; set; }

        public RecommendationContext()
        {
            Database.EnsureCreatedAsync().Wait();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
