using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult _AddOrEdit(int? id)
        {
            return View();
        }
    }
}