using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;
using BookStore.Application.ViewModels;

namespace BookStore.Components
{
    public class ShopingCartSummary: ViewComponent
    {
        private readonly IShoppingCartService _shopingCartService;
        public ShopingCartSummary(IShoppingCartService shopingCartService)
        {
            _shopingCartService = shopingCartService;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shopingCartService.GetShoppingCartItems();
            var shopingCartVM = new ShopingCartViewModel
            {
                ShopingCartItems = items,
                ShopingCartTotal = _shopingCartService.GetShoppingCartTotal()
            };
            return View(shopingCartVM);
        }
    }
}
