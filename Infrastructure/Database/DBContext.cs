using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    class DBContext : DbContext
    {
        public DbSet<Recommendations> Recommendations { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Friends> Friends { get; set; }

        public DBContext()
        {
            Database.EnsureCreatedAsync().Wait();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
