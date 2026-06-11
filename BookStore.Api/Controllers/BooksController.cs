using BookStore.Domain.Interfaces;
using BookStore.Shared.DTOs;
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
    }
}
