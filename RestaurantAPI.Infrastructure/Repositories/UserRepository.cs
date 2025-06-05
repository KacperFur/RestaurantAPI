using Dapper;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Infrastructure.SqlQueries;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(User user)
        {
            user.UserId = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(UserSql.Add, user);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(UserSql.Delete, new { id });
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(UserSql.GetAll);
                return result.ToList();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(UserSql.GetById, new { id });
                return result;
            }
        }

        public async Task UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(UserSql.Update, user);
            }
        }
    }
}
