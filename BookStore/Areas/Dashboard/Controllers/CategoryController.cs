using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Dashboard.Controllers
{
    // Dashboard/..
    [Area("Dashboard")]
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
            //List<Models.Category> categories = _categoryRepo.Categories.ToList();
            List<Models.Category> categories = new List<Models.Category>
            {
                new Models.Category{ Name="Hello", Description="xxx",Id = 1},
                new Models.Category{ Name="Hello", Description="xxx",Id = 2},
                new Models.Category{ Name="Hello", Description="xxx",Id = 3},
                new Models.Category{ Name="Hello", Description="xxx",Id = 4},
            };
            return Json(new { data = categories });
            ////return new JsonResult(categories);
            //return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(GetData));
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(GetData));
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(GetData));
            }
            catch
            {
                return View();
            }
        }
    }
}