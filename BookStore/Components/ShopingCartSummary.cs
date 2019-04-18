using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Models.Components
{
    public class ShopingCartSummary: ViewComponent
    {
        private readonly ShopingCart _shopingCart;
        public ShopingCartSummary(ShopingCart shopingCart)
        {
            _shopingCart = shopingCart;
        }

        public IViewComponentResult Invoke()
        {
            _shopingCart.ShopingCartItems = _shopingCart.GetShopingCartItems();

            //_shopingCart.ShopingCartItems = new List<ShopingCartItem> { new ShopingCartItem(), new ShopingCartItem() };
            var shopingCartVM = new ViewModels.ShopingCartViewModel
            {
                ShopingCart = _shopingCart,
                ShopingCartTotal = _shopingCart.GetShopingCartTotal()
            };
            return View(shopingCartVM);
        }
    }
}
