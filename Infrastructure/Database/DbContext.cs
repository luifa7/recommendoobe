using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    internal sealed class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Recommendations> Recommendations { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Notifications> Notifications { get; set; }

        public DbContext()
        {
            Database.EnsureCreatedAsync().Wait();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
