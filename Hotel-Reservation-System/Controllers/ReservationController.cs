﻿using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

public class ReservationController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Reservation
    public ActionResult Index()
    {
        var reservations = db.Reservations.Include("Customer").Include("Hotel").ToList();
        return View(reservations);
    }

    // GET: Reservation/Create
    public ActionResult Create(int roomId)
    {
        var room = db.Rooms.FirstOrDefault(r => r.Id == roomId);
        var userId = (int)Session["UserId"];
        var customer = db.Customers.SingleOrDefault(c => c.Id == userId);

        if (room == null)
        {
            return HttpNotFound();
        }

        if (customer == null)
        {
            return RedirectToAction("Index", "Hotel");
        }

        if (Session["UserId"] == null)
        {
            return RedirectToAction("LoginCustomer", "Account");
        }

        var reservationModel = new Reservation
        {
            RoomId = room.Id,
            Room = room
        };

        ViewBag.CustomerId = customer.Id;
        ViewBag.RoomId = roomId;

        if (Session["UserId"] != null)
        {
            var admin = db.Admins.FirstOrDefault(a => a.Id == userId);
            var customerId = db.Customers.FirstOrDefault(a => a.Id == userId);

            if (admin != null)
            {
                ViewBag.AdminName = admin.FirstName + " " + admin.LastName;
            }
            if (customerId != null)
            {
                ViewBag.CustomerName = customerId.FirstName + " " + customerId.LastName;
            }
        }
        return View(reservationModel);
    }

    // POST: Reservation/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LoginCustomer", "Account");
            }

            var userId = (int)Session["UserId"];
            var customer = db.Customers.SingleOrDefault(c => c.Id == userId);

            if (customer == null)
            {
                ModelState.AddModelError("", "Customer not found!");
                return View(reservation);
            }

            reservation.CustomerId = customer.Id;
            reservation.Status = ReservationStatus.Confirmed;
            reservation.RoomId = reservation.RoomId;

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return RedirectToAction("Index", "Hotel");
        }

        // ModelState geçerli değilse View'i tekrar göster
        ViewBag.RoomId = reservation.RoomId;
        return View(reservation);
    }

    // GET: Reservation/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Reservation reservation = db.Reservations.Find(id);
        if (reservation == null)
        {
            return HttpNotFound();
        }
        ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", reservation.CustomerId);
        ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", reservation.RoomId);
        return View(reservation);
    }

    // POST: Reservation/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            db.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.CustomerId = new SelectList(db.Customers, "Id", "FirstName", reservation.CustomerId);
        ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Name", reservation.RoomId);
        return View(reservation);
    }

    // GET: Reservation/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Reservation reservation = db.Reservations.Include("Customer").Include("Hotel").FirstOrDefault(r => r.Id == id);
        if (reservation == null)
        {
            return HttpNotFound();
        }
        return View(reservation);
    }

    // POST: Reservation/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Reservation reservation = db.Reservations.Find(id);
        db.Reservations.Remove(reservation);
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
