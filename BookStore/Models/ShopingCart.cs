using BookStore.Data;
using BookStore.Models.SharedModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ShopingCart : Entity
    {
        private readonly BookStoreContext _dbContext;
        private ShopingCart(BookStoreContext storeContext)
        {
            _dbContext = storeContext;
        }

        public string ShopingCartId { get; set; }
        public List<ShopingCartItem> ShopingCartItems { get; set; }

        public static ShopingCart GetShopingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = service.GetService<BookStoreContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShopingCart(context) { ShopingCartId = cartId };
        }

        public void AddToShopingCart(Book book,int qty)
        {
            var shopingCartItem = _dbContext.ShopingCartItems.SingleOrDefault(
                s => s.Book.Id == book.Id && s.ShopingCartId == ShopingCartId);

            if (shopingCartItem == null)
            {
                shopingCartItem = new ShopingCartItem
                {
                    ShopingCartId = ShopingCartId,
                    Amount = qty,
                    Book = book
                };
                _dbContext.ShopingCartItems.Add(shopingCartItem);
            }
            else
            {
                shopingCartItem.Amount++;
            }
            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(Book book)
        {
            var shopingCartItem = _dbContext.ShopingCartItems.SingleOrDefault(
                s => s.Book.Id == book.Id && s.ShopingCartId == ShopingCartId);

            var localAmount = 0;

            if (shopingCartItem != null)
            {
                if (shopingCartItem.Amount > 1)
                {
                    shopingCartItem.Amount--;
                    localAmount = shopingCartItem.Amount;
                }
                else
                {
                    _dbContext.ShopingCartItems.Remove(shopingCartItem);
                }
            }
            _dbContext.SaveChanges();

            return localAmount;
        }

        public List<ShopingCartItem> GetShopingCartItems()
        {
            return ShopingCartItems ??
                (ShopingCartItems =
                _dbContext.ShopingCartItems.Where(s => s.ShopingCartId == ShopingCartId)
                .Include(c => c.Book).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.ShopingCartItems
                .Where(c => c.ShopingCartId == ShopingCartId);

            _dbContext.ShopingCartItems.RemoveRange(ShopingCartItems);
            _dbContext.SaveChanges();
        }

        public decimal GetShopingCartTotal()
        {
            var total = _dbContext.ShopingCartItems
                .Where(t => t.ShopingCartId == ShopingCartId)
                .Select(t => t.Book.Price * t.Amount).Sum();
            return total;
        }

    }
}
