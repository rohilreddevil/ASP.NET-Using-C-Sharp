using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class AudioController : Controller
    {
       public Manager m = new Manager();

        // GET: Audio
        public ActionResult Index()
        {
            return View("index", "home");
        }

        // GET: Audio/Details/5
        [Route("Audio/{Id}")]
        public ActionResult Details(int? Id)
        {
            var o = m.AudioGetById(Id.GetValueOrDefault());
            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return File(o.Audio, o.AudioContentType);
            }
        }

    }

}