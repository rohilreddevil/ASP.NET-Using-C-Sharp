using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment1.EntityModels;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Map from Employee design model to EmployeeBaseViewModel. 
                cfg.CreateMap<Employee, EmployeeBaseViewModel>();

                // Map from Employee to Employee view model.         
                cfg.CreateMap<EmployeeAddViewModel, Employee>();
                //once a customer is added, it will add the new object in the data context

                //newly added mapping - milestone 2
                cfg.CreateMap<EmployeeBaseViewModel, EmployeeEditFormViewModel>();

                //newly added mapping, - milestone 2 - TRACKS
                cfg.CreateMap<Track, TrackBaseViewModel>();

                //newly added mapping - milestone 3 - INVOICES
                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();

                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>();

                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();

                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>();


            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<EmployeeBaseViewModel> EmployeeGetAll()
        //returns a collection of base objects
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>(ds.Employees).OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
        }

        public EmployeeBaseViewModel EmployeeGetById(int id)
        {

            var obj = ds.Employees.Find(id);
            //attempts to find a matching employee with the id sent as parameter

            return obj == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(obj);
            //if a match is found, MAP the Employee entity to a View Model 
            //return the newly converted view model
            //if NOT found, return NULL
        }

        public EmployeeBaseViewModel EmployeeAddNew(EmployeeAddViewModel newEmp)
        {
            var addedItem = ds.Employees.Add(mapper.Map<EmployeeAddViewModel, Employee>(newEmp));
            //Add() anticipates an object of type Employee added to the data store
            ds.SaveChanges();
            //upon adding, save changes to the data store


            // If successful, return the added item (mapped to a view model class).     
            return addedItem == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(addedItem);
        }

        //edit-existing method
        //Updates the existing employee object and saves changes to the data store

        public EmployeeBaseViewModel EmployeeEditContactInfo(EmployeeEditViewModel employee)
        {     // Attempt to fetch the object.     
            var obj = ds.Employees.Find(employee.EmployeeId);

            if (obj == null)
            {         // Employee was not found, return null.         
                return null;
            }
            else
            {    // Employee was found.  Update the entity object         
                // with the incoming values then save the changes.         
                ds.Entry(obj).CurrentValues.SetValues(employee);
                ds.SaveChanges();

                // Prepare and return the object.         
                return mapper.Map<Employee, EmployeeBaseViewModel>(obj);
            } //return type will be an Employee Base View Model 
        }

        //TRACK METHODS START HERE

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var obj = ds.Tracks.OrderBy(t => t.AlbumId).ThenBy(t => t.Composer);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(obj);
            
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllJazz()
        {
            var jazz = ds.Tracks.Where(t => t.GenreId == 2).OrderBy(t => t.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(jazz);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllRogerGlover()
        {
            var roger = ds.Tracks.Where(t => t.Composer.Contains("Roger Glover")).OrderBy(t => t.AlbumId).ThenBy(t => t.TrackId);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(roger);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllTop50Longest()
        {
            var fifty = ds.Tracks.OrderByDescending(t => t.Milliseconds).Take(50);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(fifty);
        }

        //Milestone3
        public IEnumerable<InvoiceBaseViewModel> InvoiceGetAll()
        //returns a collection of base objects
        {
            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBaseViewModel>>(ds.Invoices).OrderByDescending(e => e.InvoiceDate);
        } 

        //Milestone3
        public InvoiceBaseViewModel InvoiceGetById(int id)
        {

            var obj = ds.Invoices.Find(id);
            //attempts to find a matching invoice with the id sent as parameter

            return obj == null ? null : mapper.Map<Invoice, InvoiceBaseViewModel>(obj);
            //if a match is found, MAP the Invoice entity to a View Model 
            //return the newly converted view model
            //if NOT found, return NULL
        }

        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int id) //populate Customer and Employee Details for the gien invoice id
        {
            var obj = ds.Invoices.Include("Customer.Employee").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").SingleOrDefault(i => i.InvoiceId == id); //matching id
            return obj == null ? null : mapper.Map<Invoice, InvoiceWithDetailViewModel>(obj);
        }
    }
}