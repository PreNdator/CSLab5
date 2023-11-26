
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;
using System.Reflection.Emit;

namespace Data
{
    class GameDb: DbContext
    {
        DbSet<Achievement> Achievements { get; set; } = null!;
        DbSet<Kingdom> Kingdoms { get; set; } = null!;
        DbSet<Player> Players { get; set; } = null!;
        DbSet<PlayerStats> PlayerStats { get; set; } = null!;

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
