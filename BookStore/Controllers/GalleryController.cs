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
        public ActionResult Index(string Category,string filterBy , int pageNo = 1)
        {
            int totalPages = 6;
            int excludeRecords = (totalPages * pageNo) - totalPages;
            //var bookList = _bookRepository.Books;
            IEnumerable<Book> books;

            //text conversion to =>Title Case<=
            //TextInfo text = new CultureInfo("en-us", true).TextInfo;
            //string _category = (category != null) ? text.ToTitleCase(category) : category;

            string _currentCategory = string.Empty;

            books = _bookRepository.Books
                .OrderBy(o => o.Id)
                .Skip(excludeRecords)
                .Take(totalPages);
            _currentCategory = "All Books";
            var lengthItem = _bookRepository.Books.Count();

            GalleryViewModel galleryView = new GalleryViewModel
            {
                currentCategory = _currentCategory,
                Books = books,
                currentPage = pageNo,
                totalPages = Convert.ToInt32(Math.Ceiling(lengthItem / (double)totalPages)),
                pageSize = totalPages,
                Categories = _categoryRepository.Categories.OrderBy(s => s.Name)
            };

            return View(galleryView);
        }
    }
}