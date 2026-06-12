using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }

        Book GetBookById(int id);
        bool AddOrEdit(Book book);
        bool Delete(int id);
    }
}
