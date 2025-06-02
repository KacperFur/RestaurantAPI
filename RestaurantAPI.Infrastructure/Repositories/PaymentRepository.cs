using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;
        public PaymentRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task AddAsync(Payment payment)
        {
            var sql = @"
            INSERT INTO payments (payment_id, order_id, amount, method, status, paid_at)
            VALUES (@PaymentId, @OrderId, @Amount, @Method, @Status, @PaidAt);";

            payment.PaymentId = Guid.NewGuid();
            payment.Status = PaymentStatus.Paid.ToString();
            payment.PaidAt = DateTime.UtcNow;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, payment);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM payments WHERE id = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, new { id });
            }
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            var sql = "SELECT * FROM payments";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Payment>(sql);
                return result.ToList();
            }
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM payments WHERE id = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Payment>(sql, new { id });
            }
        }

        public async Task UpdateAsync(Payment payment)
        {
            var sql = @"
            UPDATE payments
            SET order_id = @OrderId,
                amount = @Amount,
                method = @Method,
                status = @Status,
                paid_at = @PaidAt
            WHERE id = @Id";


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, payment);
            }
        }
    }
}
