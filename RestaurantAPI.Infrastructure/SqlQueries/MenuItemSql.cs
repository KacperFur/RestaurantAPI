using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.SqlQueries
{
    internal static class MenuItemSql
    {
        internal const string Add = @"
            INSERT INTO menu_items 
            (menu_item_id, name,
            description, price, 
            category_id, meal_type,
            created_at)
            VALUES (@MenuItemId, @Name,
            @Description, @Price,
            @CategoryId, @MealType,
            @CreatedAt);";

        internal const string GetById = @"SELECT * FROM menu_items where id = @Id";

        internal const string GetAll = "SELECT * FROM menu_items";

        internal const string Delete = "DELETE FROM menu_items WHERE id = @Id";

        internal const string Update = @"
            UPDATE menu_items 
            SET name = @Name,
                description = @Description,
                price = @Price,
                meal_type = @MealType,
                updated_at = @UpdatedAt  
            WHERE id = @Id";
    }
}
