using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using Microsoft.Data.SqlClient;
using Dapper;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Infrastructure.SqlQueries;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public MenuItemRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            menuItem.MenuItemId = Guid.NewGuid();
            menuItem.CreatedAt = DateTime.UtcNow;

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(MenuItemSql.Add, menuItem);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(MenuItemSql.Delete, new { id });
            }
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<MenuItem>(MenuItemSql.GetAll);
                return result.ToList();
            }
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<MenuItem>(MenuItemSql.GetById, new { id });
                return result;
            }
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            menuItem.UpdatedAt = DateTime.UtcNow;

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(MenuItemSql.Update, menuItem);
            }
        }
    }
}
