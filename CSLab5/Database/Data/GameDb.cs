using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model;

namespace Data
{
    public class GameDb : DbContext
    {
        public DbSet<Achievement> Achievements { get; set; } = null!;
        public DbSet<Kingdom> Kingdoms { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<PlayerStats> PlayerStats { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
