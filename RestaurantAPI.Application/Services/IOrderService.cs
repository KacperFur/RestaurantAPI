using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Services
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(int id);
        void Create(Order order);
        bool Update(int id, Order order);
        bool Delete(int id);
    }
}
