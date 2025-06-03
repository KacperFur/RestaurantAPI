using RestaurantAPI.Entities;

namespace RestaurantAPI.Domain.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(int id);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(int id);
    }
}
