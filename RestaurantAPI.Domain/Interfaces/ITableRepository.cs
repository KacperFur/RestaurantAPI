using RestaurantAPI.Entities;

namespace RestaurantAPI.Domain.Interfaces
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<List<Table>> GetAllAsync();
        Task<Table> GetByIdAsync(int id);
        Task AddAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeleteAsync(int id);
    }
}
