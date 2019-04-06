using BookStore.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Book> Books { get; set; }
    }
}
