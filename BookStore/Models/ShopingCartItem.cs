using BookStore.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ShopingCartItem : Entity
    {
        public int Amount { get; set; }
        public int ShopingCartId { get; set; }

        public Book Book { get; set; }

    }
}
