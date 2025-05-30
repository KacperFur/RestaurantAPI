using Dapper;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Domain.Interfaces;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;
        public UserRepository(IConfiguration config)
        {
           _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }
        public async Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            var result = await _db.QueryAsync<User>(sql);
            return result.ToList();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM Users where id = @id";
            var result = await _db.QueryAsync<User>(sql, new {id});
            return result.SingleOrDefault();
        }

        public async Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
