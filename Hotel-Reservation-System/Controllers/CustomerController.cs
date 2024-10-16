﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.DAL;

namespace YourNamespace.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            bool isAdmin = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isAdmin = db.Admins.Any(a => a.Id == userId);
            }

            ViewBag.IsAdmin = isAdmin;
            var customers = db.Customers.ToList();
            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Müşteriyi veritabanından al
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            bool isAdmin = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isAdmin = db.Admins.Any(a => a.Id == userId);
            }

            ViewBag.IsAdmin = isAdmin;
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            bool isAdmin = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isAdmin = db.Admins.Any(a => a.Id == userId);
            }

            ViewBag.IsAdmin = isAdmin;
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (customer.Password != ConfirmPassword)
                {
                    ViewBag.PasswordMismatch = "Passwords do not match!";
                    return View(customer);
                }
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            bool isAdmin = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isAdmin = db.Admins.Any(a => a.Id == userId);
            }

            ViewBag.IsAdmin = isAdmin;
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
        {
            var existingCustomer = db.Customers.SingleOrDefault(a => a.Id == customer.Id);

            if (existingCustomer != null)
            {
                    existingCustomer.FirstName = customer.FirstName;
                    existingCustomer.LastName = customer.LastName;
                    existingCustomer.Email = existingCustomer.Email;
                    existingCustomer.Password = existingCustomer.Password;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.EditedDate = DateTime.Now;

                db.Entry(existingCustomer).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
        return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            bool isAdmin = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isAdmin = db.Admins.Any(a => a.Id == userId);
            }

            ViewBag.IsAdmin = isAdmin;

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
