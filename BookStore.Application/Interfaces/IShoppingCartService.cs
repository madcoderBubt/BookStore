using BookStore.Domain.Entities;
using System.Collections.Generic;

namespace BookStore.Application.Interfaces
{
    public interface IShoppingCartService
    {
        void AddToCart(Book book, int amount);
        int RemoveFromCart(Book book);
        List<ShopingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
    }
}
