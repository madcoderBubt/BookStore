using BookStore.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class CategoryMenu:ViewComponent
    {
        private ICategoryRepository _categoryReository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryReository = categoryRepository;

        }

        public IViewComponentResult Invoke()
        {
            var items = _categoryReository.Categories.OrderBy(s => s.Name);
            return View(items);
        }
    }
}
