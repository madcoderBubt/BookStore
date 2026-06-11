using BookStore.Domain.Entities;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Book> RelatedBooks { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
