namespace Blackjack.Database
{
    public class DbContextFactory
    {
        private readonly string connectionString;
        public DbContextFactory()
        {
            var configuration = new Configuration();
            connectionString = configuration.ConnectionString;
        }
        public DatabaseContext Create()
        {
            return new DatabaseContext(connectionString);
        }
    }
}
