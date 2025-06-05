using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using RestaurantAPI.Infrastructure.SqlQueries;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public OrderRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(Order order)
        {
            order.OrderId = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending.ToString();

            using var connection =_connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                order.Id = await connection.ExecuteScalarAsync<int>(OrderSql.OrderAddSql, order, transaction);

                foreach (var item in order.OrderItems)
                {
                    item.OrderItemId = Guid.NewGuid();
                    item.OrderId = order.Id; 
                    item.CreatedAt = DateTime.UtcNow;

                    await connection.ExecuteAsync(OrderSql.OrderItemAddSql, item, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(OrderSql.DeleteOrderItems, new { id }, transaction);
                        await connection.ExecuteAsync(OrderSql.DeleteOrder, new { id }, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var orderDict = new Dictionary<int, Order>();

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            var result = await connection.QueryAsync<Order, OrderItem, MenuItem, Order>(
                OrderSql.GetAll,
                (order, orderItem, menuItem) =>
                {
                    if (!orderDict.TryGetValue(order.Id, out var currentOrder))
                    {
                        currentOrder = order;
                        currentOrder.OrderItems = new List<OrderItem>();
                        orderDict.Add(order.Id, currentOrder);
                    }

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
            var orderDictionary = new Dictionary<int, Order>();

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                var result = await connection.QueryAsync<Order, OrderItem, MenuItem, Order>(
                    OrderSql.GetById,
                    (order, orderItem, menuItem) =>
                    {
                        if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
                        {
                            currentOrder = order;
                            currentOrder.OrderItems = new List<OrderItem>();
                            orderDictionary.Add(order.Id, currentOrder);
                        }

                        if (orderItem != null)
                        {
                            orderItem.MenuItem = menuItem;
                            currentOrder.OrderItems.Add(orderItem);
                        }

                        return currentOrder;
                    },
                    new { Id = id },
                    splitOn: "order_item_id,menu_item_id"
                );

                return orderDictionary.Values.FirstOrDefault();
            }
        }


        public async Task UpdateAsync(Order order)
        {
            order.UpdatedAt = DateTime.UtcNow;
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(OrderSql.Update, order);
            }
        }
    }
}
