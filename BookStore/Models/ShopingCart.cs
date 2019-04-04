using BookStore.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ShopingCart : Entity
    {
        public List<ShopingCartItem> ShopingCartItems { get; set; }
    }
}
