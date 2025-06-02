using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Application.Interfaces
{
    public interface ITableService
    {
        Task<List<TableDto>> GetAll();
        Task<TableDto> GetById(int id);
        Task<int> Create(CreateTableDto table);
        Task<bool> Update(int id, UpdateTableDto table);
        Task<bool> Delete(int id);
    }
}
