using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Application.ViewModels;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class ShopingCartController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCartService _shopingCartService;
        public ShopingCartController(IBookRepository bookRepository, IShoppingCartService shopingCartService)
        {
            _bookRepository = bookRepository;
            _shopingCartService = shopingCartService;
        }

        // GET: ShopingCart
        public ViewResult Index()
        {
            var items = _shopingCartService.GetShoppingCartItems();
            var shopingCartVM = new ShopingCartViewModel
            {
                ShopingCartItems = items,
                ShopingCartTotal = _shopingCartService.GetShoppingCartTotal()
            };

            return View(shopingCartVM);
        }

        // GET: ShopingCart/AddToShopingCart
        public RedirectToActionResult AddToShopingCart(int bookId, int qty = 1)
        {
            var selectedBook = _bookRepository.Books.FirstOrDefault(b => b.Id == bookId);
            if (selectedBook != null)
            {
                _shopingCartService.AddToCart(selectedBook, qty);
            }
            return RedirectToAction("Gallery", "Book", new { category = selectedBook?.Category?.Name });
        }

        public RedirectToActionResult RemoveFromShopingCart(int bookId)
        {
            var selectedBook = _bookRepository.Books.FirstOrDefault(b => b.Id == bookId);
            if (selectedBook != null)
            {
                _shopingCartService.RemoveFromCart(selectedBook);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            _shopingCartService.ClearCart();
            return RedirectToAction("Index");
        }
        
    }
}
