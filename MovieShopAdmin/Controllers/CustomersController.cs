﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieShopAdmin.Models.Customers;
using MovieShopBackend;
using MovieShopBackend.Contexts;
using MovieShopBackend.Entities;
using AutoMapper;

namespace MovieShopAdmin.Views
{
    public class CustomersController : Controller
    {
        private IManager<Customer> _manager = new ManagerFacade().GetCustomerManager();
        
        // GET: Customers
        public ActionResult Index()
        {
            return View(_manager.ReadAll());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            
            Customer customer = _manager.ReadOne(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,StreetName,StreetNumber,Country,ZipCode")] PostCustomersCreateViewModel postCustomersCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                //Use AutoMapper to copy properties from ViewModel to Entities.
                Customer customer = Mapper.Map<Customer>(postCustomersCreateViewModel);
                Address address = Mapper.Map<Address>(postCustomersCreateViewModel);
                customer.Address = address;
                
                _manager.Create(customer);
                             
                return RedirectToAction("Index");
            }

            return View(postCustomersCreateViewModel);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            
            Customer customer = _manager.ReadOne(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _manager.Update(customer);                
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            
            Customer customer = _manager.ReadOne(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = _manager.ReadOne(id);
            _manager.Delete(customer.Id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
