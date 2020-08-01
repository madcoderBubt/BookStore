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

        public bool AddEdit(Category category)
        {
            if (category == null) throw new NullReferenceException();

            try
            {
                if (category.Id == 0)
                {
                    _storeContext.Add(category);
                    _storeContext.SaveChanges();
                    return true;
                }
                else
                {
                    _storeContext.Entry(category).State = EntityState.Modified;
                    _storeContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    _storeContext.Remove(_storeContext.Categories.Find(id));
                    _storeContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category GetCategoryById(int categoryId) => _storeContext.Categories
            .FirstOrDefault(c => c.Id == categoryId);
    }
}
