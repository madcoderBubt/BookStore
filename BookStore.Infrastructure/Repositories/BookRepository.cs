using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStore.Infrastructure.Data.BookStoreContext _bookStoreContext;

        public BookRepository(BookStore.Infrastructure.Data.BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        public IEnumerable<Book> Books => _bookStoreContext.Books.Include(f=>f.Category);

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
                if (book.Id == 0)
                {
                    _bookStoreContext.Add(book);
                }
                else
                {
                    _bookStoreContext.Entry(book).State = EntityState.Modified;
                }
                _bookStoreContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _bookStoreContext.Remove(book);
                _bookStoreContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
