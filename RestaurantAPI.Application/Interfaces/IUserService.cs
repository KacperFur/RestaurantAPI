using RestaurantAPI.Application.Models;

namespace RestaurantAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<int>Create(CreateUserDto dto);
        Task<bool> Update(int id, UpdateUserDto dto);
        Task<bool> Delete(int id);
    }
}
