using Blackjack.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blackjack.Database
{
    public class DatabaseContext: DbContext
    {
        private string connectionString;
        public DatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
        public DbSet<Stats> Stats { get; set; }
    }
}
