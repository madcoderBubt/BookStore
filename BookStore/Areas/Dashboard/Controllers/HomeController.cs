using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Flag = "dashboard";
            //var list = _categoryRepo.Categories;
            return View();
        }

        // GET: Books
        public IActionResult Categories()
        {
            ViewBag.Flag = "product";
            ViewBag.subFlag = "category";
            //var listItem = _categoryRepo.Categories;
            //return View(listItem);
            return View();
        }

        // GET: Books
        public IActionResult Books()
        {
            ViewBag.Flag = "product";
            ViewBag.subFlag = "book";
            //var listItem = _bookRepo.Books;
            return View();
        }

        public IActionResult GuestMessages()
        {
            ViewBag.Flag = "message";
            return View();
        }
    }
}