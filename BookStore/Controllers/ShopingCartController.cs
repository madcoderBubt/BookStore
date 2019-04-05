using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Interface;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class ShopingCartController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ShopingCart _shopingCart;
        public ShopingCartController(IBookRepository bookRepository, ShopingCart shopingCart)
        {
            _bookRepository = bookRepository;
            _shopingCart = shopingCart;
        }

        // GET: ShopingCart
        public ViewResult Index()
        {
            //Getting Shoping Cart Items
            var items = _shopingCart.GetShopingCartItems();
            _shopingCart.ShopingCartItems = items;
            //Setting Shoping Cart View Model
            var shopingCartVM = new ShopingCartViewModel
            {
                ShopingCart = _shopingCart,
                ShopingCartTotal = _shopingCart.GetShopingCartTotal()
            };

            return View(shopingCartVM);
        }

        // GET: ShopingCart/AddToShopingCart
        public RedirectToActionResult AddToShopingCart(int bookId)
        {
            var selectedBook = _bookRepository.Books.FirstOrDefault(b => b.Id == bookId);
            if (selectedBook != null)
            {
                _shopingCart.AddToShopingCart(selectedBook, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShopingCart(int bookId)
        {
            var selectedBook = _bookRepository.Books.FirstOrDefault(b => b.Id == bookId);
            if (selectedBook != null)
            {
                _shopingCart.RemoveFromCart(selectedBook);
            }
            return RedirectToAction("Index");
        }
        
    }
}