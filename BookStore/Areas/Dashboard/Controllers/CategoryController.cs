using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.Domain.Entities;

namespace BookStore.Areas.Dashboard.Controllers
{
    // Dashboard/..
    [Area("Dashboard")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }

        // GET: Dashboard/Category/GetData
        public ActionResult GetData()
        {
            List<BookStore.Domain.Entities.Category> categories = _categoryRepo.Categories.ToList();
            return Json(new { data = categories });
        }

        // POST: Category/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult _AddOrEdit(BookStore.Domain.Entities.Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepo.AddEdit(category))
            {
                return Json(new { success = true, message = "Saved Successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Saved Unsuccessfull" });
            }
        }

        // GET: Dashboard/Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_categoryRepo.Delete(id))
            {
                return Json(new { success = true, message = "Deleted Successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Deleted Failed" });
            }
        }
    }
}