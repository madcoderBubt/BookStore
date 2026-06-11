using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private readonly BookStoreContext _dbContext;
        public string ShoppingCartId { get; set; }
        public List<ShopingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCartRepository(BookStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddToCart(Book book, int amount)
        {
            var shoppingCartItem = _dbContext.ShopingCartItems.SingleOrDefault(
                s => s.Book.Id == book.Id && s.ShopingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShopingCartItem
                {
                    ShopingCartId = ShoppingCartId,
                    Amount = amount,
                    Book = book
                };
                _dbContext.ShopingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(Book book)
        {
            var shoppingCartItem = _dbContext.ShopingCartItems.SingleOrDefault(
                s => s.Book.Id == book.Id && s.ShopingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _dbContext.ShopingCartItems.Remove(shoppingCartItem);
                }
            }
            _dbContext.SaveChanges();

            return localAmount;
        }

        public List<ShopingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _dbContext.ShopingCartItems
                .Where(s => s.ShopingCartId == ShoppingCartId)
                .Include(c => c.Book).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.ShopingCartItems
                .Where(c => c.ShopingCartId == ShoppingCartId);

            _dbContext.ShopingCartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _dbContext.ShopingCartItems
                .Where(t => t.ShopingCartId == ShoppingCartId)
                .Select(t => t.Book.Price * t.Amount).Sum();
            return total;
        }
    }
}
