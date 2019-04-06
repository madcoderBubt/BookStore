using BookStore.Data.Interface;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly BookStoreContext _storeContext;
        public CategoryRepository(BookStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IEnumerable<Category> Categories => _storeContext.Categories;

        public Category GetCategoryById(int categoryId) => _storeContext.Categories
            .FirstOrDefault(c => c.Id == categoryId);
    }
}
