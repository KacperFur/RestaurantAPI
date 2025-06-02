using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly string _connectionString;
        public TableRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task AddAsync(Table table)
        {
            var sql = @"INSERT INTO tables (table_id, table_number, seats, status)
                    VALUES (@TableId, @TableNumber, @Seats, @Status);";

            table.TableId = Guid.NewGuid();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, table);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM tables WHERE id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<List<Table>> GetAllAsync()
        {
            var sql = "SELECT * FROM tables";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return (await connection.QueryAsync<Table>(sql)).ToList();
            }
        }

        public async Task<Table> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM tables WHERE id = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Table>(sql, new { Id = id });
            }
        }

        public async Task UpdateAsync(Table table)
        {
            var sql = @"
            UPDATE tables
            SET table_number = @TableNumber,
                seats = @Seats,
                status = @Status
            WHERE id = @Id";

            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, table);
            }
        }
    }
}
