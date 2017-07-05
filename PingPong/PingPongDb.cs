
using Microsoft.EntityFrameworkCore;

namespace PlayersAPI
{
    public class PingPongDb : DbContext
    {
        // Reference our players table using this
        public DbSet<PlayerData> Players { get; set; }  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Players.db");
        }
    }
}