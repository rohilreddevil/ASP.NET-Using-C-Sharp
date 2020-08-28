using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class HomeController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is where you get an insight on the course I'm taking. Believe me, it's ...(wait for it) LEGENDARY!!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "My Contact Page";

            return View();
        }
    }
}