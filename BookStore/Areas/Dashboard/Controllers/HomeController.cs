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
            //var list = _categoryRepo.Categories;
            return View();
        }

        // GET: Books
        public IActionResult Categories()
        {
            //var listItem = _categoryRepo.Categories;
            //return View(listItem);
            return View();
        }

        // GET: Books
        public IActionResult Books()
        {
            //var listItem = _bookRepo.Books;
            return View();
        }

        public IActionResult GuestMessages()
        {
            return View();
        }
    }
}