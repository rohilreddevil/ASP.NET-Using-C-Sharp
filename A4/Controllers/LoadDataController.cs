using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment4.Controllers
{
    //[Authorize(Roles = "User")]
   // [Authorize(Roles = "Admin")]

   //DO NOT FORGET TO ENABLE [Authorize] BEFORE SUBMITTING
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RoleClaim()
        {
            if (m.LoadData())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data already exists");
            }
        }

        public ActionResult Genre()
        {
            if (m.LoadGenre())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data already exists");
            }
        }

        public ActionResult Artist()
        {
            if (m.LoadArtist())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data already exists");
            }
        }

        public ActionResult Album()
        {
            if (m.LoadAlbum())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data already exists");
            }
        }

        public ActionResult Track()
        {
            if (m.LoadTrack())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data already exists");
            }
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}