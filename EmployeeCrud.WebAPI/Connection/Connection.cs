using EmployeeCrud.Domain.IConnection;

namespace EmployeeCrud.WebAPI.Connection
{
    public class Connection: IConnection
    {
        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot Configuration = builder.Build();
            var connectionString = Configuration.GetSection("ConnectionStrings:sqlConnection");

            return connectionString.Value;
        }
    }
}
