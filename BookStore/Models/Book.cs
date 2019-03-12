using BookStore.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book : Entity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Details { get; set; }
        public string ImgUrl { get; set; }
        public bool Active { get; set; }
        public int Price { get; set; }
        public int PriceOffer { get; set; }
    }
}
