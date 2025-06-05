using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.SqlQueries
{
    internal static class OrderSql
    {
        internal const string OrderAddSql = @"
                            INSERT INTO orders 
                            (order_id, user_id, order_date, total_amount, status, created_at) 
                            OUTPUT INSERTED.id
                            VALUES (@OrderId, @UserId, @OrderDate, @TotalAmount, @Status, @CreatedAt);";

        internal const string OrderItemAddSql = @"
                                INSERT INTO order_items 
                                (order_item_id, order_id, menu_item_id, quantity, price, created_at) 
                                VALUES (@OrderItemId, @OrderId, @MenuItemId, @Quantity, @Price, @CreatedAt);";

        internal const string GetById = @"SELECT 
                    o.id, o.order_id, o.user_id, o.order_date, o.status, o.total_amount,
                    oi.id, oi.order_item_id, oi.menu_item_id, oi.quantity, oi.price,
                    mi.id, mi.menu_item_id, mi.name, mi.description, mi.price, mi.category_id, mi.meal_type
                    FROM orders o
                    LEFT JOIN order_items oi ON o.id = oi.order_id
                    LEFT JOIN menu_items mi ON oi.menu_item_id = mi.id
                    WHERE o.id = @Id
                    ";

        internal const string GetAll = @"SELECT 
                    o.id, o.order_id, o.user_id, o.order_date, o.status, o.total_amount,
                    oi.id, oi.order_item_id, oi.menu_item_id, oi.quantity, oi.price,
                    mi.id, mi.menu_item_id, mi.name, mi.description, mi.price, mi.category_id, mi.meal_type
                    FROM orders o
                    LEFT JOIN order_items oi ON o.id = oi.order_id
                    LEFT JOIN menu_items mi ON oi.menu_item_id = mi.id";

        internal const string DeleteOrderItems = "DELETE FROM order_items WHERE order_id = @id";
        internal const string DeleteOrder = "DELETE FROM orders WHERE id = @id";

        internal const string Update = @"
            UPDATE orders 
            SET customer_id = @CustomerId,
                order_date = @OrderDate,
                total_amount = @TotalAmount,
                status = @Status,
                updated_at = @UpdatedAt  
            WHERE order_id = @OrderId";
    }
}
