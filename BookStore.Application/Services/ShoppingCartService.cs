using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System.Collections.Generic;

namespace BookStore.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartService(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public void AddToCart(Book book, int amount)
        {
            _shoppingCart.AddToCart(book, amount);
        }

        public int RemoveFromCart(Book book)
        {
            return _shoppingCart.RemoveFromCart(book);
        }

        public List<ShopingCartItem> GetShoppingCartItems()
        {
            return _shoppingCart.GetShoppingCartItems();
        }

        public void ClearCart()
        {
            _shoppingCart.ClearCart();
        }

        public decimal GetShoppingCartTotal()
        {
            return _shoppingCart.GetShoppingCartTotal();
        }
    }
}
