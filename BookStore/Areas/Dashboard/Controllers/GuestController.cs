using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class GuestController : Controller
    {
        private readonly IGuestMessage _message;
        public GuestController(IGuestMessage message)
        {
            _message = message;
        }

        public IActionResult GetData()
        {
            var dataLists = _message.GuestMessages.ToList();
            //var lists = JsonConvert.SerializeObject(
            //    dataLists,
            //    Formatting.Indented,
            //    new JsonSerializerSettings()
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });

            return Json(new { data = dataLists});
        }
    }
}
