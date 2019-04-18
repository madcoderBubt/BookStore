using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Interface;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        public readonly IOrderRepository _orderRepository;
        public readonly ShopingCart _shopingCart;

        public IActionResult Checkout()
        {
            return View();
        }
    }
}