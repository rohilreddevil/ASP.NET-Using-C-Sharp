using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class ArtistController : Controller
    {
        //reference the manager
        private Manager m = new Manager();

        [Authorize] 
        //DO NOT FORGET TO ENABLE AUTHORIZE BEFORE SUBMISSION

        // GET: Artist
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var t = m.ArtistGetById(id.GetValueOrDefault());

            if (t == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(t);
            }
        }

        // GET: Artist/Create

        [Authorize(Roles = "Executive")] 
        public ActionResult Create()
        {
            var form = new ArtistAddFormViewModel();

            // Configure the SelectList for the item-selection element on the HTML Form
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

            return View(form);
        }

        // POST: Artist/Create

        [HttpPost]
        [Authorize(Roles = "Executive")] 
        public ActionResult Create(ArtistAddViewModel newArtist)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newArtist);

            try
            {
                // Process the input
                var addedItem = m.ArtistAdd(newArtist);

                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newArtist);
                else
                    //if successful, redirect to the Details View
                    return RedirectToAction("Details", new { id = addedItem.Id });
                //The returned object will have the unique identifier that was assigned 
            }
            catch
            {
                return View(newArtist);
            }
        }

        //AddAlbum Methods - 1

        [Authorize(Roles = "Coordinator")] 
        // DO NOT FORGET TO AUTHORIZE BEFORE SUBMISSION
        [Route("Artist/{id}/AddAlbum")]
        public ActionResult AddAlbum(int? id)
        {
            // Attempt to get the associated object
            var a = m.ArtistGetById(id.GetValueOrDefault());

            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure a form object
                var form = new AlbumAddFormViewModel();
                form.ArtistName = a.Name;
                form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                var selectedValues = new List<int> { a.Id };

                form.ArtistList = new MultiSelectList
                    (items: m.ArtistGetAll(),
                    dataValueField: "Id",
                    dataTextField: "Name",
                    selectedValues: selectedValues);

                form.TracksList = new MultiSelectList
                    (items: m.TrackGetAllByArtistId(id.GetValueOrDefault()),
                    dataValueField: "Id",
                    dataTextField: "Name");

                return View(form);
            }
        }

        //AddAlbum Method 2 - Post
        [Authorize(Roles = "Coordinator")] 
        //DO NOT FORGET TO AUTHORIZE BEFORE SUBMISSION
        [Route("Artist/{id}/AddAlbum")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                
                return RedirectToAction("Details", "Album", new { id = addedItem.Id });
            }
        }


        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
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

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
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
