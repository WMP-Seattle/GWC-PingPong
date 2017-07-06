
using Microsoft.EntityFrameworkCore;
using PingPong.Entities;

namespace PingPong
{
    public class PingPongDb : DbContext
    {
        // Reference our players table using this
        public DbSet<Player> Players { get; set; }  
        public DbSet<Game> Games { get; set; }  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./PingPong.db");
        }
    }
}