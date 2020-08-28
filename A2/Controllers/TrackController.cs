using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASSIGNMENT_2.Models;

namespace ASSIGNMENT_2.Controllers
{
    public class TrackController : Controller
    {
        //reference the manager
        private Manager m = new Manager();

        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAllWithDetail());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? id)
        {
            var t = m.TrackGetByIdWithDetail(id.GetValueOrDefault());

            if (t == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(t);
            }
            
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            /*var form = new TrackAddFormViewModel();

            // Configure the SelectList for the item-selection element on the HTML Form
            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");

            // Configure the SelectList for the item-selection element on the HTML Form
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");

            return View(form);*/

            // Create a form
            var form = new TrackAddFormViewModel();

            // Configure the SelectList for the item-selection element on the HTML Form
            form.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");

            // Configure the SelectList for the item-selection element on the HTML Form
            form.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");

            return View(form);
        }

        // POST: Track/Create - package the data

        [HttpPost]
        public ActionResult Create(TrackAddViewModel newTrack)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newTrack);

            try
            {
                // Process the input
                var addedItem = m.TrackAdd(newTrack);

                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newTrack);
                else
                    //if successful, redirect to the Details View
                    return RedirectToAction("Details", new { id = addedItem.TrackId });
                //The returned object will have the unique identifier that was assigned 
            }
            catch
            {
                return View(newTrack);
            }
        }

        // GET: Track/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Track/Edit/5
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

        // GET: Track/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Track/Delete/5
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
