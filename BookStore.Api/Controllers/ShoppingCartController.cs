using BookStore.Domain.Interfaces;
using BookStore.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IBookRepository bookRepository, IShoppingCart shoppingCart)
        {
            _bookRepository = bookRepository;
            _shoppingCart = shoppingCart;
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCart(string cartId)
        {
            _shoppingCart.ShoppingCartId = cartId;
            var items = _shoppingCart.GetShoppingCartItems();

            var dto = new ShoppingCartDto
            {
                Items = items.Select(i => new CartItemDto
                {
                    Id = i.Id,
                    BookId = i.Book.Id,
                    BookName = i.Book.Name,
                    Price = i.Book.Price,
                    Amount = i.Amount,
                    ImgUrl = i.Book.ImgUrl
                }).ToList(),
                Total = (int)_shoppingCart.GetShoppingCartTotal()
            };

            return Ok(dto);
        }

        [HttpPost("{cartId}/add/{bookId}")]
        public IActionResult AddToCart(string cartId, int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null) return NotFound();

            _shoppingCart.ShoppingCartId = cartId;
            _shoppingCart.AddToCart(book, 1);

            return Ok();
        }

        [HttpDelete("{cartId}/remove/{bookId}")]
        public IActionResult RemoveFromCart(string cartId, int bookId)
        {
            var book = _bookRepository.GetBookById(bookId);
            if (book == null) return NotFound();

            _shoppingCart.ShoppingCartId = cartId;
            _shoppingCart.RemoveFromCart(book);

            return Ok();
        }

        [HttpDelete("{cartId}")]
        public IActionResult ClearCart(string cartId)
        {
            _shoppingCart.ShoppingCartId = cartId;
            _shoppingCart.ClearCart();
            return Ok();
        }
    }
}
