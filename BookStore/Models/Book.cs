using BookStore.Models.SharedModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book : Entity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Details { get; set; }
        [Display(Name ="Upload Img")]
        [NotMapped]
        public IFormFile ImgFile { get; set; }
        public string ImgUrl { get; set; }
        public bool Active { get; set; }
        public int Price { get; set; }
        public int PriceOffer { get; set; }

       
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
