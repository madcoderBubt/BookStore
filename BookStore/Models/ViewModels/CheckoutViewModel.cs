using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        //public ShopingCartViewModel ShopingCartVM { get; set; }
        public ShopingCart ShopingCart { get; set; }
        public decimal ShopingCartTotal { get; set; }
    }
}
