using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class TracksController : Controller
    {

        private Manager m = new Manager();

        
        public ActionResult Index(String filter)
           
        {
            return View(m.TrackGetAll());
          
        }

        public ActionResult Jazz() 
        {
            return View("Index", m.TrackGetAllJazz());
        }

        public ActionResult Roger()
        {
            return View("Index", m.TrackGetAllRogerGlover());
        }
      
        public ActionResult LongestFifty()
        {
            return View("Index", m.TrackGetAllTop50Longest());
        }

        // GET: Tracks/Details/5
       /* public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tracks/Create
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

        // GET: Tracks/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tracks/Edit/5
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

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
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
        } */
    }
}
