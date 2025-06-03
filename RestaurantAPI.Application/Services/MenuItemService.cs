using AutoMapper;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;
        private readonly IMapper _mapper;
        public MenuItemService(IMenuItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Create(CreateMenuItemDto dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _repository.AddAsync(menuItem);
            return menuItem.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var menuItem = await _repository.GetByIdAsync(id);
            if (menuItem == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<MenuItemDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<MenuItemDto>>(result);
        }

        public async Task<MenuItemDto> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<MenuItemDto>(result);
        }

        public async Task<bool> Update(int id, UpdateMenuItemDto dto)
        {
            var existingMenuItem = await _repository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                return false;
            }

            _mapper.Map(dto, existingMenuItem);
            await _repository.UpdateAsync(existingMenuItem);
            return true;
        }
    }
}
