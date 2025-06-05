using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.SqlQueries
{
    internal static class UserSql
    {
        internal static string Add = @"
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

        internal static string GetById = @"SELECT * FROM Users where id = @Id";

        internal static string GetAll = "SELECT * FROM Users";

        internal static string Delete = "DELETE FROM Users WHERE id = @id";

        internal static string Update = @"
            UPDATE Users 
            SET first_name = @FirstName,
                last_name = @LastName,
                username = @Username,
                password_hash = @PasswordHash,
                email = @Email,
                role_id = @RoleId,
                updated_at = @UpdatedAt
            WHERE id = @Id";
    }
}