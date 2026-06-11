using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Application.ViewModels;
using BookStore.Infrastructure.Data;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMessageRepository _guestMessage;
        public HomeController(IBookRepository bookRepository, ICategoryRepository categoryRepository, IMessageRepository guestMessage)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _guestMessage = guestMessage;
        }

        public IActionResult Index()
        {
            IEnumerable<HomeViewModel> homeVM = _categoryRepository.Categories
                .Where(s => s.Id != 0)
                .Select(s => new HomeViewModel{
                    Name = s.Name,
                    PreferredBooks = _bookRepository.Books
                    .Where(p => p.CategoryId == s.Id)
                    .OrderByDescending(p=>p.Price)
                    .Take(2)
                });
            return View(homeVM);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (message != null)
            {
                if (_guestMessage.SendMessage(message))
                    return View();
            }
            return null;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
