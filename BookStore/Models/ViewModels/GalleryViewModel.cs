using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class GalleryViewModel
    {
        public string currentCategory { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
    }
}
