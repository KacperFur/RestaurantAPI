using AutoMapper;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Create(CreateOrderDto dto)
        {
           var order = _mapper.Map<Order>(dto);
           await _repository.AddAsync(order);
           return order.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<OrderDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<OrderDto>>(result);
        }

        public async Task<OrderDto> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(result);
        }

        public async Task<bool> Update(int id, UpdateOrderDto dto)
        {
            var existingOrder = await _repository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return false;
            }

            _mapper.Map(dto, existingOrder);
            await _repository.UpdateAsync(existingOrder);
            return true;
        }
    }
}
