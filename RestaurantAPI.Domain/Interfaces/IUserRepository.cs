using RestaurantAPI.Entities;

namespace RestaurantAPI.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);    
        Task DeleteAsync(int id);
    }
}
