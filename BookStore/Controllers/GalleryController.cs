 using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Interface;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Mvc;

namespace BookStore.Controllers
{
    public class GalleryController : Controller
    {
        //private BookStoreContext _bookStoreContext;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        public GalleryController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        //GET: Book
        public ActionResult Index(string c, string fb, int pn = 1)
        {
            int totalPages = 6;
            int lengthItem;
            int excludeRecords = (totalPages * pn) - totalPages;
            IEnumerable<Book> books, books1;

            //text conversion to => Title Case <=
             TextInfo text = new CultureInfo("en-us", true).TextInfo;
            string _category = (c != null) ? text.ToTitleCase(c) : c;

            string _currentCategory = string.Empty;

            if (string.IsNullOrEmpty(c) || c.Equals("All Books"))
            {
                books = _bookRepository.Books;
                _currentCategory = "All Books";
            }
            else
            {
                books = _bookRepository.Books
                    .Where(f => f.Category.Name == _category);
                _currentCategory = c;
            }
            if (!string.IsNullOrEmpty(fb))
            {
                string[] s = fb.Split('-');
                for (int i = 0; i < s.Length; i++)
                {
                    s[i] = s[i].Trim(new char[] { '$',' '});
                }
                books1 = books
                    .Where(f => 
                    f.Price >= Convert.ToInt32(s[0], CultureInfo.InvariantCulture) && 
                    f.Price <= Convert.ToInt32(s[1], CultureInfo.InvariantCulture));
                books = books1;
            }
            lengthItem = books.Count();

            ViewBag.filter = fb;
            GalleryViewModel galleryView = new GalleryViewModel
            {
                currentCategory = _currentCategory,
                Books = books
                    .OrderBy(o => o.Id)
                    .Skip(excludeRecords)
                    .Take(totalPages),
                currentPage = pn,
                totalPages = Convert.ToInt32(Math.Ceiling(lengthItem / (double)totalPages)),
                pageSize = totalPages,
                Categories = _categoryRepository.Categories.OrderBy(s => s.Name)
            };

            return View(galleryView);
        }
    }
}