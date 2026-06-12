using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;

        public OrdersController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        [HttpPost("checkout/{cartId}")]
        public IActionResult Checkout(string cartId, [FromBody] OrderDto orderDto)
        {
            _shoppingCart.ShoppingCartId = cartId;
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (items.Count == 0)
            {
                return BadRequest("Your cart is empty");
            }

            var order = new Order
            {
                FirstName = orderDto.FirstName,
                LastName = orderDto.LastName,
                Address = orderDto.Address,
                ZipCode = orderDto.ZipCode,
                State = orderDto.State,
                Country = orderDto.Country,
                PhoneNumber = orderDto.PhoneNumber,
                Email = orderDto.Email,
                OrderTotal = _shoppingCart.GetShoppingCartTotal(),
                OrderedPlaced = DateTime.Now
            };

            _orderRepository.CreateOrder(order);
            _shoppingCart.ClearCart();

            return Ok(new { Message = "Order placed successfully", OrderId = order.Id });
        }
    }
}
