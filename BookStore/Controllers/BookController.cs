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

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        //private BookStoreContext _bookStoreContext;
        private IBookRepository _bookRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BookController(IBookRepository bookRepository, IHostingEnvironment hostingEnvironment)
        {
            _bookRepository = bookRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Book
        public ActionResult Index()
        {
            var bookList = _bookRepository.Books;
            return View(bookList);
        }

        // GET: Book
        public ActionResult Gallery()
        {
            var bookList = _bookRepository.Books;
            return View(bookList);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var bookItem = _bookRepository.GetBookById(id);
            return View(bookItem);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    //book.Id = 1;
                    book.Active = true;
                    if (book.ImgFile != null)
                    {
                        //Destination FileName
                        var fileName = Path.Combine(_hostingEnvironment.WebRootPath + "/images/", Path.GetFileName(book.ImgFile.FileName));
                        FileStream fileStream = new FileStream(fileName, FileMode.Create);
                        //Coping File to Server
                        book.ImgFile.CopyTo(fileStream);
                        book.ImgUrl = "/images/" + Path.GetFileName(fileName);
                    }
                    else
                    {
                        book.ImgUrl = "/images/ggg.jpg";
                        //throw new Exception("File Not found");
                    }

                    _bookRepository.Add(book);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}