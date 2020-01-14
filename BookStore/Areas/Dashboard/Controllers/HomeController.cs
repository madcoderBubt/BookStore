using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View();
        }

        // GET: Books
        public IActionResult Categories()
        {
            return View();
        }

        // GET: Books
        public IActionResult Books()
        {
            return View();
        }
    }
}