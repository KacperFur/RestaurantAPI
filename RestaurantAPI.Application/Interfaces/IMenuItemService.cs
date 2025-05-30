using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Interfaces
{
    public interface IMenuItemService
    {
        List<MenuItem> GetAll();
        MenuItem GetById(int id);
        void Create(MenuItem menuItem);
        bool Update(int id, MenuItem menuItem);
        bool Delete(int id);
    }
}
