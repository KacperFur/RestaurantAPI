using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly string _connectionString;
        public MenuItemRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task AddAsync(MenuItem menuItem)
        {
            menuItem.MenuItemId = Guid.NewGuid();
            menuItem.CreatedAt = DateTime.UtcNow;
            var sql = @"
            INSERT INTO menu_items 
            (menu_item_id, name,
            description, price, 
            category_id, meal_type,
            created_at)
            VALUES (@MenuItemId, @Name,
            @Description, @Price,
            @CategoryId, @MealType,
            @CreatedAt);";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, menuItem);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM menu_items WHERE id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, new { id });
            }
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            var sql = "SELECT * FROM menu_items";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<MenuItem>(sql);
                return result.ToList();
            }
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM menu_items where id = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<MenuItem>(sql, new { id });
                return result.SingleOrDefault();
            }
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            menuItem.UpdatedAt = DateTime.UtcNow;
            var sql = @"
            UPDATE menu_items 
            SET name = @Name,
                description = @Description,
                price = @Price,
                meal_type = @MealType,
                updated_at = @UpdatedAt  
            WHERE id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, menuItem);

            }
        }
    }
}
