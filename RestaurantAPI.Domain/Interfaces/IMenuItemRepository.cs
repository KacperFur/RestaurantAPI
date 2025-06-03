using RestaurantAPI.Entities;

namespace RestaurantAPI.Domain.Interfaces
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        Task<List<MenuItem>> GetAllAsync();
        Task<MenuItem> GetByIdAsync(int id);
        Task AddAsync(MenuItem menuItem);
        Task UpdateAsync(MenuItem menuItem);
        Task DeleteAsync(int id);
    }
}
