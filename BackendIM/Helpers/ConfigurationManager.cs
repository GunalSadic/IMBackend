using System;

namespace BackendIM.Helpers
{
    public sealed class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> _instance =
            new Lazy<ConfigurationManager>(() => new ConfigurationManager());

        private ConfigurationManager()
        {
            AppName = "MessagingApp";
            MaxConnections = 100;
            DatabaseConnectionString = "Server=localhost;Database=MessagingApp;User Id=Gunal;Password=GunalPassword123;";
        }

        public static ConfigurationManager Instance => _instance.Value;

        public string AppName { get; private set; }
        public int MaxConnections { get; private set; }
        public string DatabaseConnectionString { get; private set; }

        public void UpdateSettings(string appName, int maxConnections, string connectionString)
        {
            AppName = appName;
            MaxConnections = maxConnections;
            DatabaseConnectionString = connectionString;
        }

        public void PrintConfiguration()
        {
            Console.WriteLine($"App Name: {AppName}");
            Console.WriteLine($"Max Connections: {MaxConnections}");
            Console.WriteLine($"Database Connection String: {DatabaseConnectionString}");
        }
    }
}