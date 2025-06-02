using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task AddAsync(Order order)
        {
            var orderSql = @"
INSERT INTO orders 
(order_id, user_id, order_date, total_amount, status, created_at) 
OUTPUT INSERTED.id
VALUES (@OrderId, @UserId, @OrderDate, @TotalAmount, @Status, @CreatedAt);";

            var orderItemSql = @"
INSERT INTO order_items 
(order_item_id, order_id, menu_item_id, quantity, price, created_at) 
VALUES (@OrderItemId, @OrderId, @MenuItemId, @Quantity, @Price, @CreatedAt);";

            order.OrderId = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending.ToString();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                order.Id = await connection.ExecuteScalarAsync<int>(orderSql, order, transaction);

                foreach (var item in order.OrderItems)
                {
                    item.OrderItemId = Guid.NewGuid();
                    item.OrderId = order.Id; 
                    item.CreatedAt = DateTime.UtcNow;

                    await connection.ExecuteAsync(orderItemSql, item, transaction);
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task DeleteAsync(int id)
        {
            var deleteOrderItemsSql = "DELETE FROM order_items WHERE order_id = @id";
            var deleteOrderSql = "DELETE FROM orders WHERE id = @id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(deleteOrderItemsSql, new { id }, transaction);
                        await connection.ExecuteAsync(deleteOrderSql, new { id }, transaction);
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var sql = @"SELECT 
                    o.id, o.order_id, o.user_id, o.order_date, o.status, o.total_amount,
                    oi.id, oi.order_item_id, oi.menu_item_id, oi.quantity, oi.price,
                    mi.id, mi.menu_item_id, mi.name, mi.description, mi.price, mi.category_id, mi.meal_type
                FROM orders o
                LEFT JOIN order_items oi ON o.id = oi.order_id
                LEFT JOIN menu_items mi ON oi.menu_item_id = mi.id";

            var orderDict = new Dictionary<int, Order>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var result = await connection.QueryAsync<Order, OrderItem, MenuItem, Order>(
                sql,
                (order, orderItem, menuItem) =>
                {
                    if (!orderDict.TryGetValue(order.Id, out var currentOrder))
                    {
                        currentOrder = order;
                        currentOrder.OrderItems = new List<OrderItem>();
                        orderDict.Add(order.Id, currentOrder);
                    }

                    // orderItem.Id może być 0 tylko jeśli nie istnieje (LEFT JOIN)
                    if (orderItem != null && orderItem.Id != 0)
                    {
                        orderItem.MenuItem = (menuItem != null && menuItem.Id != 0) ? menuItem : null;

                        if (!currentOrder.OrderItems.Any(x => x.Id == orderItem.Id))
                        {
                            currentOrder.OrderItems.Add(orderItem);
                        }
                    }

                    return currentOrder;
                },
                splitOn: "id,id"
            );

            return orderDict.Values.ToList();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM orders WHERE id = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Order>(sql, new { id });
                return result.SingleOrDefault();
            }
        }

        public async Task UpdateAsync(Order order)
        {
            var sql =
                @"UPDATE orders
                 SET user_id = @UserId,
                order_date = @OrderDate,
                status = @Status,
                total_amount = @TotalAmount
                WHERE id = @Id";
            order.UpdatedAt = DateTime.UtcNow;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, order);
            }
        }
    }
}
