using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Services
{
    public interface IPaymentService
    {
        List<Payment> GetAll();
        Payment GetById(int id);
        void Create(Payment payment);
        bool Update(int id, Payment payment);
        bool Delete(int id);
    }
}
