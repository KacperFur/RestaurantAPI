using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        void Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }
}
