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
        //c => Category | fb => FilterBook | pn => pageNumber
        public ActionResult Index(string c, string fb, string src, int pn = 1)
        {
            int totalPages = 6;
            int lengthItem;
            int excludeRecords = (totalPages * pn) - totalPages;
            IEnumerable<Book> books, books1;

            //text conversion to => Title Case <=
             TextInfo text = new CultureInfo("en-us", true).TextInfo;
            string _category = (c != null) ? text.ToTitleCase(c) : c;

            string _currentCategory = string.Empty;

            //getting books by catergories
            if (string.IsNullOrEmpty(c) || c == "All Books")
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

            //searching for specific book
            if (!string.IsNullOrEmpty(src))
            {
                books1 = books.Where(f => f.Name.Contains(src, StringComparison.InvariantCultureIgnoreCase));
                books = books1;
            }

            //filtering books by price range
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
                Categories = _categoryRepository.Categories
                    .Take(15)
                    .OrderBy(s => s.Name)
            };

            return View(galleryView);
        }

        public ActionResult Book(int id)
        {
            //Book book = new Book();

            var book = _bookRepository.GetBookById(id);

            BookViewModel bookView = new BookViewModel
            {
                Book = book,
                RelatedBooks = _bookRepository.Books
                    .Where(f => f.CategoryId == book.CategoryId)
                    .Take(3)
                    .OrderBy(s=>s.Name),
                Categories = _categoryRepository.Categories
                    .Take(15)
                    .OrderBy(s => s.Name)
            };

            return View(bookView);
        }
    }
}