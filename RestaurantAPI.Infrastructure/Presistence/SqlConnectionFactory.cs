using Microsoft.Extensions.Configuration;
using RestaurantAPI.Application.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RestaurantAPI.Infrastructure.Presistence
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
