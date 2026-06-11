using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Application.ViewModels;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartService _shopingCartService;

        public OrderController(IOrderRepository orderRepository, IShoppingCartService shopingCartService)
        {
            _orderRepository = orderRepository;
            _shopingCartService = shopingCartService;
        }

        //[Authorize]
        public IActionResult Checkout()
        {
            var items = _shopingCartService.GetShoppingCartItems();

            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Cart is Empty. Add some Books first.");
            }

            CheckoutViewModel orderView = new CheckoutViewModel
            {
                ShopingCartItems = items,
                ShopingCartTotal = _shopingCartService.GetShoppingCartTotal()
            };
            return View(orderView);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult CheckOut(Order order)
        {
            var items = _shopingCartService.GetShoppingCartItems();

            if (order == null) throw new NullReferenceException();

            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Cart is Empty. Add some Books first.");
            }
            if (ModelState.IsValid)
            {
                order.OrderedPlaced = DateTime.Now;
                order.OrderTotal = _shopingCartService.GetShoppingCartTotal();

                _orderRepository.CreateOrder(order);
                _shopingCartService.ClearCart();

                return Json(new { success = true, message = "Saved Successfull" });
            }
            return Json(new { success = false, message = "Saved Unsuccessfull" });
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.Message = "Thanks for your order.";
            
            return View();
        }

    }
}
