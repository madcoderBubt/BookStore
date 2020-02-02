using BookStore.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BookStore.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BookController(IBookRepository book, ICategoryRepository category, IHostingEnvironment environment)
        {
            _bookRepo = book;
            _categoryRepo = category;
            _hostingEnvironment = environment;
        }

        //Get: /Dashboard/Book/GetData
        public IActionResult GetData()
        {
            var listItem = _bookRepo.Books;
            var lists = JsonConvert.SerializeObject(
                listItem,
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return Content(lists, "application/json");
        }

        //Get: /Dashboard/Book/_AddOrEdit
        public IActionResult AddOrEdit(int? id)
        {
            var catagories = _categoryRepo.Categories.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(CultureInfo.CurrentCulture),
                Text = a.Name
            }).ToList();
            ViewData["Catagories"] = catagories;

            if (id == null)
            {
                return View();
            }
            else
            {
                var book = _bookRepo.Books.FirstOrDefault(f => f.Id == id);
                return View(book);
            }
        }

        //Post: /Dashboard/Book/_AddOrEdit
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(Models.Book book)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    
                    if (_bookRepo.AddOrEdit(book))
                    {
                        return Json(new { success = true, message = "Saved Successfull" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Saved Failed" });
                    }
                }

                return Json(new { success = false, message = "Model state is invalid!" });
            }
            catch (Exception)
            {
                //return Json(new { success = false, message = e.Message });
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            if (_bookRepo.Delete(id))
            {
                return Json(new { success = true, message = "Delete Successfull" });
            }
            return Json(new { success = false, message = "Delete Failed" });
        }
    }
}