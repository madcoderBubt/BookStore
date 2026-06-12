using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        public List<ShopingCartItem> ShopingCartItems { get; set; }
        public decimal ShopingCartTotal { get; set; }
    }
}
