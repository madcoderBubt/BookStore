using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Interface
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        IEnumerable<OrderDetail> GetOrderDetails(int orderId);
        Order GetOrderById(int id);

        void CreateOrder(Order order);
    }
}
