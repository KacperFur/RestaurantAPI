using RestaurantAPI.Entities;

namespace RestaurantAPI.Domain.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
    }
}
