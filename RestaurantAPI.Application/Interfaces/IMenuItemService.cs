using RestaurantAPI.Application.Models;

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
