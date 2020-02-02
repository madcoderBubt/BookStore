using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Data.Interface;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models.ViewModels;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        //private BookStoreContext _bookStoreContext;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        //GET: Book
        public ActionResult Index()
        {
            var bookList = _bookRepository.Books;
            //var v = new PaginatedList()
            return View(bookList);
        }

        // GET: Book
        public ActionResult Gallery(string category)
        {
            //var bookList = _bookRepository.Books;
            IEnumerable<Book> books;

            //text conversion to =>Title Case<=
            TextInfo text = new CultureInfo("en-us", true).TextInfo;
            string _category = (category !=null) ? text.ToTitleCase(category):category;

            string _currentCategory = string.Empty;
            if (string.IsNullOrEmpty(category))
            {
                books = _bookRepository.Books.OrderBy(o => o.Id);
                _currentCategory = "All Books";
            }
            else
            {
                books = _bookRepository.Books.Where(f => f.Category.Name.Equals(_category)).OrderBy(f => f.Id);
                _currentCategory = _category;
            }

            GalleryViewModel galleryView = new GalleryViewModel
            {
                currentCategory = _currentCategory,
                Books = books,
                Categories = _categoryRepository.Categories.OrderBy(s => s.Name)
            };

            return View(galleryView);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var bookItem = _bookRepository.GetBookById(id);
            return View(bookItem);
        }
    }
}