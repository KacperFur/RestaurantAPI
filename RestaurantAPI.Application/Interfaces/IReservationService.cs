using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Application.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAll();
        Task<ReservationDto> GetById(int id);
        Task<int> Create(CreateReservationDto reservation);
        Task<bool> Update(int id, UpdateReservationDto reservation);
        Task<bool> Delete(int id);
    }
}
