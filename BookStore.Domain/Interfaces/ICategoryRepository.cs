using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface ICategoryRepository
    {

        IEnumerable<Category> Categories { get; }

        Category GetCategoryById(int categoryId);
        bool AddEdit(Category category);
        bool Delete(int id);
    }
}
