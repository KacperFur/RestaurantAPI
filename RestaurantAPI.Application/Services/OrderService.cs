using RestaurantAPI.Application.Interfaces;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Application.Services
{
    public class OrderService : IOrderService
    {
        private List<Order> orders = new List<Order>
        {
            new Order(1, 1, 1, DateTime.Now, OrderStatus.Pending, 19.99m, new List<OrderItem>
            {
                new OrderItem(1, 1, 1, 2, 19.99m),
                new OrderItem(2, 1, 2, 1, 5.99m)
            }),
            new Order(2, 2, 2, DateTime.Now, OrderStatus.Completed, 15.99m, new List<OrderItem>
            {
                new OrderItem(3, 2, 3, 1, 7.99m),
                new OrderItem(4, 2, 4, 1, 8.00m)
            }),
            new Order(3, 3, 3, DateTime.Now, OrderStatus.Cancelled, 12.99m, new List<OrderItem>
            {
                new OrderItem(5, 3, 5, 1, 12.99m)
            })
        };

        public void Create(Order order)
        {
            orders.Add(order);
        }

        public bool Delete(int id)
        {
            var order = orders.FirstOrDefault(e => e.Id == id);
            if (order == null)
            {
                return false;
            }

            orders.Remove(order);
            return true;
        }

        public List<Order> GetAll()
        {
            return orders;
        }

        public Order GetById(int id)
        {
            return orders.FirstOrDefault(e => e.Id == id);
        }

        public bool Update(int id, Order order)
        {
            var existingOrder = orders.FirstOrDefault(e => e.Id == id);
            if (existingOrder == null)
            {
                return false;
            }
            existingOrder.UserId = order.UserId;
            //existingOrder.DishId = order.DishId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            //existingOrder.OrderItems = order.OrderItems;
            return true;
        }
    }
}
