using BookStore.Data.Interface;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private BookStoreContext _bookStoreContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BookRepository(BookStoreContext storeContext, IHostingEnvironment hosting)
        {
            _bookStoreContext = storeContext;
            _hostingEnvironment = hosting;
        }

        public IEnumerable<Book> Books => _bookStoreContext.Books.Include(f=>f.Category);
        //public Book Book => _bookStoreContext;

        public Book GetBookById(int id) => _bookStoreContext.Books.Include(s=>s.Category).FirstOrDefault(p => p.Id == id);
        public Category GetCategory(int id) => _bookStoreContext.Books.FirstOrDefault(p => p.Id == id).Category;

        public bool AddOrEdit(Book book)
        {
            if (book == null)
            {
                return false;
            }

            try
            {
                if (book != null && book.ImgFile != null)
                {
                    string number = string.Format(
                        CultureInfo.CurrentCulture,
                        "{0:d9}",
                        (DateTime.Now.Ticks / 10) % 1000000000);

                    //Destination FileName
                    var fileName = Path.Combine(_hostingEnvironment.WebRootPath + "/images/books/", number + Path.GetExtension(book.ImgFile.FileName));
                    //FileStream fileStream = new FileStream(fileName, FileMode.Create);
                    using (var strm = System.IO.File.Create(fileName))
                    {
                        book.ImgFile.CopyTo(strm);
                    }
                    book.ImgUrl = "/images/books/" + number + Path.GetExtension(fileName);
                }

                if (book.Id == 0)
                {
                    _bookStoreContext.Add(book);
                    _bookStoreContext.SaveChanges();
                    return true;
                }
                else
                {
                    _bookStoreContext.Entry(book).State = EntityState.Modified;
                    _bookStoreContext.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
                throw;
            }
        }

        public bool Delete(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _bookStoreContext.Remove(book);
                _bookStoreContext.SaveChanges();

                var fileName = Path.Combine(_hostingEnvironment.WebRootPath + book.ImgUrl);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                return true;
            }

            return false;
        }
    }
}
