using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Data.Interface;
using BookStore.Data.Repositories;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Dashboard.Controllers
{
    [Authorize]
    [Area("Dashboard")]
    public class OrderController : Controller
    {
        //private readonly BookStoreContext storeContext;
        private readonly IOrderRepository orderRepo;

        public OrderController(IOrderRepository orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            List<Order> OrderLists = orderRepo.Orders.ToList();
            return Json(new { data = OrderLists});
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            var orderDetails = orderRepo.GetOrderById(id);
            return View(orderDetails);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
