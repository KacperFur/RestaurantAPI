using AutoMapper;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Create(CreateReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            await _repository.AddAsync(reservation);
            return reservation.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var reservation = await _repository.GetByIdAsync(id);
            if (reservation == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ReservationDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<ReservationDto>>(result);
        }

        public async Task<ReservationDto> GetById(int id)
        {
            var result = _repository.GetByIdAsync(id);
            return _mapper.Map<ReservationDto>(result);
        }

        public async Task<bool> Update(int id, UpdateReservationDto dto)
        {
            var existingReservation = await _repository.GetByIdAsync(id);
            if (existingReservation == null)
            {
                return false;
            }

            _mapper.Map(dto, existingReservation);
            await _repository.UpdateAsync(existingReservation);
            return true;
        }
    }
}
