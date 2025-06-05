using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.SqlQueries
{
    internal static class PaymentSql
    {
        internal static string Add = @"
            INSERT INTO payments 
            (payment_id, order_id,
            amount, payment_date,
            payment_method, status,
            created_at)
            VALUES (@PaymentId, @OrderId,
            @Amount, @PaymentDate,
            @PaymentMethod, @Status,
            @CreatedAt);";

        internal static string GetById = @"SELECT * FROM payments WHERE id = @Id";

        internal static string GetAll = "SELECT * FROM payments";

        internal static string Delete = "DELETE FROM payments WHERE id = @Id";

        internal static string Update = @"
            UPDATE payments 
            SET order_id = @OrderId,
                amount = @Amount,
                payment_date = @PaymentDate,
                payment_method = @PaymentMethod,
                status = @Status,
                updated_at = @UpdatedAt  
            WHERE id = @Id";
    }
}
