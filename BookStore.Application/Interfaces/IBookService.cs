using BookStore.Application.DTOs;
using System.Collections.Generic;

namespace BookStore.Application.Interfaces
{
    public interface IBookService
    {
        bool AddOrEdit(BookDto bookDto);
        bool Delete(int id);
    }
}
