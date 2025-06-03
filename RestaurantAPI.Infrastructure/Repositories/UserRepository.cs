using Dapper;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using Microsoft.Data.SqlClient;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task AddAsync(User user)
        {
            user.UserId = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            var sql = @"
                INSERT INTO Users 
                (user_id, first_name, 
                last_name, username, 
                password_hash, email, 
                role_id, created_at)
                VALUES 
                (@UserId, @FirstName, 
                 @LastName, @Username, 
                 @PasswordHash, @Email, 
                @RoleId, @CreatedAt);";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, user);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM Users WHERE id = @id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, new { id });
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<User>(sql);
                return result.ToList();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM Users where id = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<User>(sql, new { id });
                return result.SingleOrDefault();
            }
        }

        public async Task UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            var sql = @"
            UPDATE Users 
            SET first_name = @FirstName,
                last_name = @LastName,
                username = @Username,
                password_hash = @PasswordHash,
                email = @Email,
                role_id = @RoleId,
                updated_at = @UpdatedAt
            WHERE id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, user);
            }
        }
    }
}
