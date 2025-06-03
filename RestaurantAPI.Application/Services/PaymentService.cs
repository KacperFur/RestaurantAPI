using AutoMapper;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Create(CreatePaymentDto dto)
        {
            var payment = _mapper.Map<Payment>(dto);
            await _repository.AddAsync(payment);
            return payment.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<PaymentDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<PaymentDto>>(result);
        }

        public async Task<PaymentDto> GetById(int id)
        {
            var result = _repository.GetByIdAsync(id);
            return _mapper.Map<PaymentDto>(result);
        }

        public async Task<bool> Update(int id, UpdatePaymentDto dto)
        {
            var existingPayment = await _repository.GetByIdAsync(id);
            if (existingPayment == null)
            {
                return false;
            }

            _mapper.Map(dto, existingPayment);
            await _repository.UpdateAsync(existingPayment);
            return true;
        }
    }
}
