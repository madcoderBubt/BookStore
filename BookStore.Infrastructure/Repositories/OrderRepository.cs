using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStore.Infrastructure.Data.BookStoreContext _storeContext;
        private readonly ShopingCart _shopingCart;
        public OrderRepository(BookStore.Infrastructure.Data.BookStoreContext storeContext,ShopingCart shopingCart)
        {
            _storeContext = storeContext;
            _shopingCart = shopingCart;
        }

        public IEnumerable<Order> Orders => _storeContext.Orders;

        public void CreateOrder(Order order)
        {
            if (order == null) throw new NullReferenceException();

            order.OrderedPlaced = DateTime.Now;
            _storeContext.Orders.Add(order);    //Add Order

            var shopingCartItems = _shopingCart.ShopingCartItems;
            foreach (var item in shopingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    BookId = item.BookId,
                    OrderId = order.Id,
                    Price = item.Book.Price
                };
                _storeContext.OrderDetails.Add(orderDetail);    //Add Order Details
            }
            _storeContext.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            var order = _storeContext.Orders
                .Include(f => f.OrderLines)
                .Where(f => f.Id == id)
                .FirstOrDefault();
            return order;
        }

        public IEnumerable<OrderDetail> GetOrderDetails(int orderId)
        {
            var orderDetails = _storeContext.OrderDetails
                .Where(f => f.OrderId == orderId);
            return orderDetails;
        }
    }
}
