using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class GalleryViewModel
    {
        public string currentCategory;
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
