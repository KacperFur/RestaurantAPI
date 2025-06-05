using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using RestaurantAPI.Infrastructure.SqlQueries;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public TableRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(Table table)
        {
            table.TableId = Guid.NewGuid();

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(TableSql.Add, table);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(TableSql.Delete, new { Id = id });
            }
        }

        public async Task<List<Table>> GetAllAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                return (await connection.QueryAsync<Table>(TableSql.GetAll)).ToList();
            }
        }

        public async Task<Table> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Table>(TableSql.GetById, new { Id = id });
            }
        }

        public async Task UpdateAsync(Table table)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(TableSql.Update, table);
            }
        }
    }
}
