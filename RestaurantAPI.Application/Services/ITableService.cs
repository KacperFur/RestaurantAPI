using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Application.Services
{
    public interface ITableService
    {
        List<Table> GetAll();
        Table GetById(int id);
        void Create(Table table);
        bool Update(int id, Table table);
        bool Delete(int id);
    }
}
