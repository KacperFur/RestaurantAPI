using AutoMapper;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Application.Models;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _repository;
        private readonly IMapper _mapper;   
        public TableService(ITableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Create(CreateTableDto dto)
        {
            var table = _mapper.Map<Table>(dto);
            await _repository.AddAsync(table);
            return table.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var table = await _repository.GetByIdAsync(id);
            if (table == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<TableDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<TableDto>>(result);
        }

        public async Task<TableDto> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<TableDto>(result);
        }

        public async Task<bool> Update(int id, UpdateTableDto dto)
        {
            var existingTable = await _repository.GetByIdAsync(id);
            if (existingTable == null)
            {
                return false;
            }

            _mapper.Map(dto, existingTable);
            await _repository.UpdateAsync(existingTable);
            return true;
        }
    }
}
