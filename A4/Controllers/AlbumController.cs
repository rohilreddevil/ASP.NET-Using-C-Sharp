using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class AlbumController : Controller
    {

        //reference the manager
        private Manager m = new Manager();

        [Authorize] 
        //DO NOT FORGET TO ENABLE AUTHORIZE BEFORE SUBMISSION

        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var t = m.AlbumGetById(id.GetValueOrDefault());

            if (t == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(t);
            }
        }

         [Authorize(Roles = "Clerk")] 
        // DO NOT FORGET TO AUTHORIZE BEFORE SUBMISSION
        [Route("Album/{id}/AddTrack")]
        public ActionResult AddTrack(int? id)
        {
            // Attempt to get the associated object
            var a = m.AlbumGetById(id.GetValueOrDefault());

            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure a form object
                var form = new TrackAddFormViewModel();
                form.AlbumName = a.Name;
                form.AlbumId = a.Id;
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                return View(form);
            }
        }

        [Authorize(Roles = "Clerk")] 
        //DO NOT FORGET TO AUTHORIZE BEFORE SUBMISSION
        [Route("Album/{id}/AddTrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TrackAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {

                return RedirectToAction("Details", "Track", new { id = addedItem.Id });
            }
        }

        // GET: Album/Create
        //[Authorize(Roles = "Executive")] - DO NOT FORGET TO AUTHORIZE BEFORE SUBMISSION
        public ActionResult Create()
        {
            return View();
        }

        // POST: Album/Create
        [HttpPost]
        //[Authorize(Roles = "Executive")] - DO NOT FORGET TO AUTHORIZE BEFORE SUBMISSION
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

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
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

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
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
