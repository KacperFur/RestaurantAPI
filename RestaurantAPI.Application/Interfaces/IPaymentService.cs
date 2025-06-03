using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentDto>> GetAll();
        Task<PaymentDto> GetById(int id);
        Task<int> Create(CreatePaymentDto payment);
        Task<bool> Update(int id, UpdatePaymentDto payment);
        Task<bool> Delete(int id);
    }
}
