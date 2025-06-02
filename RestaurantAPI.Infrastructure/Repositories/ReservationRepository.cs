using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;   

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;
        public ReservationRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task AddAsync(Reservation reservation)
        {
            var sql = @"
            INSERT INTO reservations 
            (reservation_id, table_id, user_id, reservation_time, guest_count, status, created_at)
            VALUES (@ReservationId, @TableId, @UserId, @ReservationTime, @GuestCount, @Status, @CreatedAt);";

            reservation.ReservationId = Guid.NewGuid();
            reservation.CreatedAt = DateTime.UtcNow;
            reservation.Status = ReservationStatus.Confirmed.ToString();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, reservation);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM reservations WHERE id = @id";
            using var connection = new SqlConnection(_connectionString);
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, new { id });
            }
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            var sql = "SELECT * FROM reservations";
            using var connection = new SqlConnection(_connectionString);
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Reservation>(sql);
                return result.ToList();
            }
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM reservations WHERE id = @id";

            using var connection = new SqlConnection(_connectionString);
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<Reservation>(sql, new { id });
                return result;
            }
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            reservation.UpdatedAt = DateTime.UtcNow;
            var sql = @"
            UPDATE reservations
            SET user_id = @UserId,
                table_id = @TableId,
                reservation_time = @ReservationTime,
                guest_count = @GuestCount,
                status = @Status,
                updated_at = @UpdatedAt
            WHERE id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, reservation);
            }
        }
    }
}
