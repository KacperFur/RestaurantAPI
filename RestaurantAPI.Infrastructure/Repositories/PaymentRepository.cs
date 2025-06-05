using Dapper;
using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using RestaurantAPI.Infrastructure.SqlQueries;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public PaymentRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(Payment payment)
        {
            var sql = @"
            INSERT INTO payments (payment_id, order_id, amount, method, status, paid_at)
            VALUES (@PaymentId, @OrderId, @Amount, @Method, @Status, @PaidAt);";

            payment.PaymentId = Guid.NewGuid();
            payment.Status = PaymentStatus.Paid.ToString();
            payment.PaidAt = DateTime.UtcNow;
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(PaymentSql.Add, payment);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM payments WHERE id = @id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(PaymentSql.Delete, new { id });
            }
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            var sql = "SELECT * FROM payments";
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Payment>(PaymentSql.GetAll);
                return result.ToList();
            }
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Payment>(PaymentSql.GetById, new {Id = id });
            }
        }

        public async Task UpdateAsync(Payment payment)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(PaymentSql.Update, payment);
            }
        }
    }
}
