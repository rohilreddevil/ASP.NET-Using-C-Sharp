using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASSIGNMENT_2.Controllers
{
    public class MediaTypeController : Controller
    {
        // GET: MediaType
        public ActionResult Index()
        {
            return View();
        }

        // GET: MediaType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MediaType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MediaType/Create
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

        // GET: MediaType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MediaType/Edit/5
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

        // GET: MediaType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MediaType/Delete/5
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
