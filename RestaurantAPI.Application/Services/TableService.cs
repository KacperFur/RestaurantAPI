using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class TableService : ITableService
    {
        private static List<Table> tables = new List<Table>
        {
            new Table(1, 4,2, TableStatus.Free),
            new Table(2, 2,3, TableStatus.Occupied),
            new Table(3, 6,4, TableStatus.Reserved)
        };

        public void Create(Table table)
        {
            tables.Add(table);
        }

        public bool Delete(int id)
        {
            var table = tables.FirstOrDefault(e => e.Id == id);
            if (table == null)
            {
                return false;
            }

            tables.Remove(table);
            return true;
        }

        public List<Table> GetAll()
        {
            return tables;
        }

        public Table GetById(int id)
        {
            return tables.FirstOrDefault(e => e.Id == id);
        }

        public bool Update(int id, Table table)
        {
            var existingTable = tables.FirstOrDefault(e => e.Id == id);
            if (existingTable == null)
            {
                return false;
            }
            existingTable.Seats = table.Seats;
            existingTable.TableNumber = table.TableNumber;
            existingTable.Status = table.Status;
            return true;
        }
    }
}
