using RestaurantAPI.Application.Models;
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
        Task<List<MenuItemDto>> GetAll();
        Task<MenuItemDto> GetById(int id);
        Task<int> Create(CreateMenuItemDto menuItem);
        Task<bool> Update(int id, UpdateMenuItemDto menuItem);
        Task<bool> Delete(int id);
    }
}
