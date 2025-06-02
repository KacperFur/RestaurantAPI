using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAll();
        Task<OrderDto> GetById(int id);
        Task<int> Create(CreateOrderDto order);
        Task<bool> Update(int id, UpdateOrderDto order);
        Task<bool> Delete(int id);
    }
}
