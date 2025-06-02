using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using System.Threading.Tasks;


namespace RestaurantAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository repository, IMapper mapper, IPasswordHasher<User> hasher)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = hasher;
        }

        public async Task<int> Create(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = hashedPassword;
            await _repository.AddAsync(user);

            return user.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var user = _repository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(result);
        }

        public async Task<UserDto> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<bool> Update(int id, UpdateUserDto dto)
        {
            var existingUser = await _repository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            var hashedPassword = _passwordHasher.HashPassword(existingUser, dto.Password);
            existingUser.PasswordHash = hashedPassword;
            _mapper.Map(dto, existingUser);
            await _repository.UpdateAsync(existingUser);
            return true;
        }
    }
}
