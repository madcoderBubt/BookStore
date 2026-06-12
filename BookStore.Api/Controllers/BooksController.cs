using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetBooks([FromQuery] string? category = null)
        {
            var books = _bookRepository.Books.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                books = books.Where(b => b.Category.Name == category);
            }

            var result = books.Select(b => new BookDto
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                Details = b.Details,
                ImgUrl = b.ImgUrl,
                Active = b.Active,
                Price = b.Price,
                PriceOffer = b.PriceOffer,
                CategoryId = b.CategoryId,
                CategoryName = b.Category.Name
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var b = _bookRepository.GetBookById(id);
            if (b == null) return NotFound();

            var result = new BookDto
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                Details = b.Details,
                ImgUrl = b.ImgUrl,
                Active = b.Active,
                Price = b.Price,
                PriceOffer = b.PriceOffer,
                CategoryId = b.CategoryId,
                CategoryName = b.Category?.Name
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateBook([FromBody] BookDto bookDto)
        {
            var book = new Book
            {
                Name = bookDto.Name,
                Author = bookDto.Author,
                Details = bookDto.Details,
                ImgUrl = bookDto.ImgUrl,
                Active = bookDto.Active,
                Price = bookDto.Price,
                PriceOffer = bookDto.PriceOffer,
                CategoryId = bookDto.CategoryId
            };

            _bookRepository.AddOrEdit(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return NotFound();

            book.Name = bookDto.Name;
            book.Author = bookDto.Author;
            book.Details = bookDto.Details;
            book.ImgUrl = bookDto.ImgUrl;
            book.Active = bookDto.Active;
            book.Price = bookDto.Price;
            book.PriceOffer = bookDto.PriceOffer;
            book.CategoryId = bookDto.CategoryId;

            _bookRepository.AddOrEdit(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null) return NotFound();

            _bookRepository.Delete(id);
            return NoContent();
        }
    }
}
