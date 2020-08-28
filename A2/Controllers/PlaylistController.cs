using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASSIGNMENT_2.Models;

namespace ASSIGNMENT_2.Controllers
{
    public class PlaylistController : Controller
    {
        //reference the manager
        private Manager m = new Manager();

        // GET: Playlist
        public ActionResult Index()
        {
            return View(m.PlaylistGetAll());
        }

        // GET: Playlist/Details/5
        public ActionResult Details(int? id)
        {
            var t = m.PlaylistGetById(id.GetValueOrDefault());

            if (t == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(t);
            }

        }

        // GET: Playlist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlist/Create
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

        // GET: Playlist/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.PlaylistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form, based on the fetched matching object
                var form = m.mapper.Map<PlaylistEditTracksFormViewModel>(o);

                // For the multi select list, configure the "selected" items
                // Notice the use of the Select() method, 
                // which allows us to select/return/use only some properties from the source
                var selectedValues = o.Tracks.Select(jd => jd.TrackId);

                // For clarity, use the named parameter feature of C#
                form.TrackList = new MultiSelectList
                    (items: m.TrackGetAll(),
                    dataValueField: "TrackId",
                    dataTextField: "NameShort",
                    selectedValues: selectedValues);

                return View(form);
            }
        }

        // POST: Playlist/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracksViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again
                return RedirectToAction("Edit", new { id = newItem.PlaylistId });
            }

            if (id.GetValueOrDefault() != newItem.PlaylistId)
            {
                // This appears to be data tampering, so redirect the user away
                return RedirectToAction("Index");
            }

            // Attempt to do the update
            var editedItem = m.PlaylistEdit(newItem);

            if (editedItem == null)
            {
                //if there is a problem, go to the edit page
                return RedirectToAction("Edit", new { id = newItem.PlaylistId });
            }
            else
            {
                // Show the details view, which will have the updated data
                return RedirectToAction("Details", new { id = newItem.PlaylistId });
            }
        }

        // GET: Playlist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Playlist/Delete/5
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
