using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.SqlQueries
{
    internal static class ReservationSql
    {
        internal static string Add = @"
            INSERT INTO reservations 
            (reservation_id, table_id, user_id, reservation_time, guest_count, status, created_at)
            VALUES (@ReservationId, @TableId, @UserId, @ReservationTime, @GuestCount, @Status, @CreatedAt);";

        internal static string GetById = @"SELECT * FROM reservations WHERE id = @id";

        internal static string GetAll = "SELECT * FROM reservations";

        internal static string Delete = "DELETE FROM reservations WHERE id = @id";

        internal static string Update = @"
            UPDATE reservations
            SET user_id = @UserId,
                table_id = @TableId,
                reservation_time = @ReservationTime,
                guest_count = @GuestCount,
                status = @Status,
                updated_at = @UpdatedAt
            WHERE id = @Id";
    }
}
