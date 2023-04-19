namespace Blackjack.Database
{
    public class Configuration
    {
        public string ConnectionString =>
            $"Server={Host};Port={Port};User Id={User};Password={Password};Database={DbName};SSL Mode={SslMode}";
        public string Port { get; set; } = "5432";
        public string Host { get; set; } = "horton.db.elephantsql.com";
        public string User { get; set; } = "ajghgoli";
        public string Password { get; set; } = "AT_0ZqNQ-3TnEm_BKVyDidRlO7wNPcuk";
        public string DbName { get; set; } = "ajghgoli";
        public string SslMode { get; set; } = "VerifyFull";
    }

}
