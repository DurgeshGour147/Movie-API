using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Movie_store.Common.DBConnection
{
    public class DBSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();
        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";
        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "MovieStore",
                        ProviderName = "SqlServer",
                        ConnectionString = ConfigManager.Instance.MovieStoreConnectionString
                    };
            }
        }
    }
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }
}
