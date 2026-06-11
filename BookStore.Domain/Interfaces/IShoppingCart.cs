using BookStore.Domain.Entities;
using System.Collections.Generic;

namespace BookStore.Domain.Interfaces
{
    public interface IShoppingCart
    {
        string ShoppingCartId { get; set; }
        List<ShopingCartItem> ShoppingCartItems { get; set; }
        void AddToCart(Book book, int amount);
        int RemoveFromCart(Book book);
        List<ShopingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
    }
}
