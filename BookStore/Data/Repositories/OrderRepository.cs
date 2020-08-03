using BookStore.Data.Interface;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStoreContext _storeContext;
        private readonly ShopingCart _shopingCart;
        public OrderRepository(BookStoreContext storeContext,ShopingCart shopingCart)
        {
            _storeContext = storeContext;
            _shopingCart = shopingCart;
        }

        public void CreateOrder(Order order)
        {
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

    }
}
