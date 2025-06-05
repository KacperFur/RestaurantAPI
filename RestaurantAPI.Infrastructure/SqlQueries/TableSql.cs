using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.SqlQueries
{
    internal static class TableSql
    {
        internal static string Add = @"INSERT INTO tables (table_id, table_number, seats, status)
                    VALUES (@TableId, @TableNumber, @Seats, @Status);";

        internal static string GetById = "SELECT * FROM tables WHERE id = @Id";

        internal static string GetAll = "SELECT * FROM tables";

        internal static string Delete = "DELETE FROM tables WHERE id = @Id";

        internal static string Update = @"
            UPDATE tables
            SET table_number = @TableNumber,
                seats = @Seats,
                status = @Status
            WHERE id = @Id";
    }
}
