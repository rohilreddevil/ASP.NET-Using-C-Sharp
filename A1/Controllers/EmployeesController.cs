using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class EmployeesController : Controller
    {
        //reference to a manager
        private Manager m = new Manager();
        private object newEmp;


        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object     
            var obj = m.EmployeeGetById(id.GetValueOrDefault());

            if (obj == null) return HttpNotFound();
            else return View(obj);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            EmployeeAddViewModel obj = new EmployeeAddViewModel();
            return View(obj);
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(EmployeeAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
                return View(newItem);

            try
            {
                // Process the input
                var addedItem = m.EmployeeAddNew(newItem);

                // If the item was not added, return the user to the Create page
                // otherwise redirect them to the Details page.
                if (addedItem == null)
                    return View(newItem);
                else
                    //if successful, redirect to the Details View
                    return RedirectToAction("Details", new { id = addedItem.EmployeeId });
                //The returned object will have the unique identifier that was assigned 
            }
            catch
            {
                return View(newItem);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object     
            var obj = m.EmployeeGetById(id.GetValueOrDefault());

            if (obj == null)
                return HttpNotFound();
            else
            {         // Create and configure an "HTML form" 

                // obj is a EmployeeBase object so         
                // we must map it to a EmployeeEditFormViewModel object         
                //THIS BUILDS AN HTML FORM       

                var formObj = m.mapper.Map<EmployeeBaseViewModel, EmployeeEditFormViewModel>(obj);
                //mapping the source object to the newly made form object

                return View(formObj);
            }
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditViewModel model)
        {
            // Validate the input     
            if (!ModelState.IsValid)
            {
                // Our "version 1" approach is to display the "edit form" again         
                return RedirectToAction("Edit", new { id = model.EmployeeId });
            }

            if (id.GetValueOrDefault() != model.EmployeeId)
            {         // This appears to be data tampering, so redirect the user away         
                return RedirectToAction("Index");
            }

            // Attempt to do the update     
            var editedItem = m.EmployeeEditContactInfo(model);

            if (editedItem == null)
            {
                // There was a problem updating the object         
                // Our "version 1" approach is to display the "edit form" again         
                return RedirectToAction("Edit", new { id = model.EmployeeId });
            }
            else
            {
                // Show the details view, which will show the updated data         
                return RedirectToAction("Details", new { id = model.EmployeeId });
            }

        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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
