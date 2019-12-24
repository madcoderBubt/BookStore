using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class HomeViewModel
    {
        public string Name { get; set; }
        public IEnumerable<Book> PreferredBooks { get; set; }
    }
}
