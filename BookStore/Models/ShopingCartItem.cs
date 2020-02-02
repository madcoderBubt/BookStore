using BookStore.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ShopingCartItem : Entity
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }
        public string ShopingCartId { get; set; }
    }
}
