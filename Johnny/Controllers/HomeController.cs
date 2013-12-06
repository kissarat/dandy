using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johnny.Models;

namespace Johnny.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult AboutUs()
        {
            return View();
        }

        public ViewResult Contacts()
        {
            return View();
        }

        public ContentResult Static(string name)
        {
            return Content("Empty content");
        }
    }
}
