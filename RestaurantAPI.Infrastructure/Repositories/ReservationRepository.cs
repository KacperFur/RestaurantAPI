using Dapper;
using Microsoft.Data.SqlClient;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using RestaurantAPI.Infrastructure.SqlQueries;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public ReservationRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task AddAsync(Reservation reservation)
        {
            reservation.ReservationId = Guid.NewGuid();
            reservation.CreatedAt = DateTime.UtcNow;
            reservation.Status = ReservationStatus.Confirmed.ToString();

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(ReservationSql.Add, reservation);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(ReservationSql.Delete, new { id });
            }
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Reservation>(ReservationSql.GetAll);
                return result.ToList();
            }
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Reservation>(ReservationSql.GetById, new { id });
                return result;
            }
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            reservation.UpdatedAt = DateTime.UtcNow;

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(ReservationSql.Update, reservation);
            }
        }
    }
}
