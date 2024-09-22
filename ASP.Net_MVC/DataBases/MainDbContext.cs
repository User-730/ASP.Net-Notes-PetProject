using Microsoft.EntityFrameworkCore;
using ASP.Net_MVC.Models;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASP.Net_MVC.DataBases
{
    public class MainDbContext : DbContext
    {
        public DbSet<EventAction> Events => Set<EventAction>();
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<Mark> Marks => Set<Mark>();

        public MainDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
