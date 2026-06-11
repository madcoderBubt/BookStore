using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class ShopingCart : Entity
    {
        public string ShopingCartId { get; set; }
        public List<ShopingCartItem> ShopingCartItems { get; set; }
    }
}
