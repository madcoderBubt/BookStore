using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Interface;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        public readonly IOrderRepository _orderRepository;
        public readonly ShopingCart _shopingCart;

        public OrderController(IOrderRepository orderRepository, ShopingCart shopingCart)
        {
            _orderRepository = orderRepository;
            _shopingCart = shopingCart;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CheckOut(Order order)
        {
            var items = _shopingCart.GetShopingCartItems();
            _shopingCart.ShopingCartItems = items;

            if (_shopingCart.ShopingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is Empty. Add some Books first.");
            }
            if (ModelState.IsValid)
            {
                order.OrderedPlaced = DateTime.Now;
                order.OrderTotal = _shopingCart.GetShopingCartTotal();

                _orderRepository.CreateOrder(order);
                _shopingCart.ClearCart();

                return RedirectToAction("CheckoutComplete");
            }
            return View();
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.Message = "Thanks for your order.";
            
            return View();
        }

    }
}