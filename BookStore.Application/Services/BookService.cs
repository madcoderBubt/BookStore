using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Globalization;
using System.IO;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IHostingEnvironment _webHostEnvironment;

        public BookService(IBookRepository bookRepository, IHostingEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public bool AddOrEdit(BookDto bookDto)
        {
            if (bookDto == null) return false;

            var book = new Book
            {
                Id = bookDto.Id,
                Name = bookDto.Name,
                Author = bookDto.Author,
                Details = bookDto.Details,
                Active = bookDto.Active,
                Price = bookDto.Price,
                PriceOffer = bookDto.PriceOffer,
                CategoryId = bookDto.CategoryId,
                ImgUrl = bookDto.ImgUrl
            };

            if (bookDto.ImgFile != null)
            {
                string number = string.Format(
                    CultureInfo.CurrentCulture,
                    "{0:d9}",
                    (DateTime.Now.Ticks / 10) % 1000000000);

                var fileName = Path.Combine(_webHostEnvironment.WebRootPath + "/images/books/", number + Path.GetExtension(bookDto.ImgFile.FileName));

                using (var strm = File.Create(fileName))
                {
                    bookDto.ImgFile.CopyTo(strm);
                }
                book.ImgUrl = "/images/books/" + number + Path.GetExtension(fileName);
            }

            return _bookRepository.AddOrEdit(book);
        }

        public bool Delete(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book != null)
            {
                var result = _bookRepository.Delete(id);
                if (result && !string.IsNullOrEmpty(book.ImgUrl))
                {
                    var fileName = Path.Combine(_webHostEnvironment.WebRootPath + book.ImgUrl);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
                return result;
            }
            return false;
        }
    }
}
