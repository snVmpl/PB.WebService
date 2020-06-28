using Microsoft.EntityFrameworkCore;
using PB.Data.Providers;

namespace PB.Data
{
    public class DataBaseContextFactory
    {
        public DatabaseContext CreateDatabaseContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            var options = optionsBuilder
                .UseSqlServer(ConnectionStringProvider.Instance.CurrentConnection)
                .Options;

            return new DatabaseContext(options);
        }
    }
}
