using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment5.Models;
using AutoMapper;

namespace Assignment5.Controllers
{
    public class TrackController : Controller
    {
        private Manager m = new Manager();

        [Authorize]
        // GET: Track
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Track/Details/5
        public ActionResult Details(int? Id)
        {
            var t = m.TrackGetById(Id.GetValueOrDefault());

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
            return View();
        }

        // POST: Track/Create
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

        // GET: Track/Edit/5
          [Authorize(Roles = "Clerk")]
         [Route("Track/Edit/{Id}")]
         public ActionResult Edit(int? Id)
          {
              var o = m.TrackGetById(Id.GetValueOrDefault());

              if (o == null)
              {
                  return HttpNotFound();
              }
              else
              {
                  // Create a form, based on the fetched matching object
                  var form = new TrackEditFormViewModel();
                  form.Name = o.Name;


                  return View(form);
              }
          } 


        // POST: Track/Edit/5
         [Authorize(Roles = "Clerk")]
         [Route("Track/Edit/{Id}")]
          [HttpPost]
              public ActionResult Edit(TrackEditViewModel myTrack)
              {
                  // Validate the input
                  if (!ModelState.IsValid)
                  {
                     var form = new TrackEditFormViewModel();
                     form.Name = myTrack.Name;
                     return View(form);
                 }

                  // Process the input
                  var editItem = m.TrackEdit(myTrack);

                  if (editItem == null)
                  {
                     return HttpNotFound();
                 }
                  else
                  {

                    return RedirectToAction("Details", "Track", new { id = editItem.Id });

                }
              } 


        // GET: Track/Delete/5
        [Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.TrackGetById(id.GetValueOrDefault());
            if (itemToDelete == null)
            {
                // Don't leak info about the delete attempt
                // Simply redirect
                return RedirectToAction("Index");
            }
            else
                return View(itemToDelete);
        }

        // POST: Track/Delete/5
        [Authorize(Roles = "Clerk")]
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            var result = m.TrackDelete(id.GetValueOrDefault());
            // "result" will be true or false
            // We probably won't do much with the result, because
            // we don't want to leak info about the delete attempt
            // In the end, we should just redirect to the list view
            return RedirectToAction("Index");
        }
    }
}
