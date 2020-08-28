using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class MediaItemController : Controller
    {
        public Manager m = new Manager();
        // GET: MediaItem
        public ActionResult Index()
        {
            return View();
        }

        // GET: MediaItem/Details/5

        [Route("MediaItem/{Id}")]
        public ActionResult Details(int? Id)
        {
            // Attempt to get the matching object
            var o = m.MediaGetById(Id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(o.Content, o.ContentType);
            }
        }

        // GET: MediaItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediaItem/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MediaItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MediaItem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MediaItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MediaItem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
