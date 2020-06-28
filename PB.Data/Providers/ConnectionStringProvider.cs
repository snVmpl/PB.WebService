using System.Collections.Generic;

namespace PB.Data.Providers
{
    /// <summary>
    /// Keep, set and provide connection string for switch db
    /// </summary>
    public class ConnectionStringProvider
    {
        private static ConnectionStringProvider _instance;
        private readonly Dictionary<string, string> _connectionStringDictionary = new Dictionary<string, string>();

        public string CurrentConnection { get; set; }

        public static ConnectionStringProvider Instance => _instance ??= new ConnectionStringProvider();

        public void AddConnectionString(string name, string connectionString)
        {
            _connectionStringDictionary.TryAdd(name, connectionString);
        }

        public void SetCurrentConnection(string key)
        {
            CurrentConnection = _connectionStringDictionary[key];
        }
    }
}
