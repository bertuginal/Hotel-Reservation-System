using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

public class ReservationController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Reservation
    public ActionResult Index()
    {
        var userId = (int)Session["UserId"];

        if (Session["UserId"] == null)
        {
            return RedirectToAction("LoginCustomer", "Account");
        }

        if (Session["UserId"] != null)
        {
            var admin = db.Admins.FirstOrDefault(a => a.Id == userId);
            var customer = db.Customers.FirstOrDefault(a => a.Id == userId);

            if (admin != null)
            {
                ViewBag.AdminName = admin.FirstName + " " + admin.LastName;
            }
            if (customer != null)
            {
                ViewBag.CustomerName = customer.FirstName + " " + customer.LastName;
            }
        }

        int customerId = (int)Session["UserId"];
        var reservations = db.Reservations
            .Include(r => r.Room)
            .Include(r => r.Room.Hotel)
            .Where(r => r.CustomerId == customerId)
            .ToList();

        return View(reservations);
    }


    // GET: Reservation/Create
    public ActionResult Create(int roomId)
    {
        if (Session["UserId"] == null)
        {
            return RedirectToAction("LoginCustomer", "Account");
        }

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
        if (reservation.CheckInDate < DateTime.Now)
        {
            ModelState.AddModelError("CheckInDate", "Check-in date must be today or later!");
        }

        if (reservation.CheckOutDate < reservation.CheckInDate)
        {
            ModelState.AddModelError("CheckOutDate", "Check-out date must be after check-in date!");
        }

        if (ModelState.IsValid)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("LoginCustomer", "Account");
            }

            var userId = (int)Session["UserId"];
            var customer = db.Customers.SingleOrDefault(c => c.Id == userId);
            var room = db.Rooms.SingleOrDefault(r => r.Id == reservation.RoomId);

            if (room == null)
            {
                ModelState.AddModelError("", "Room not found!");
                return View(reservation);
            }

            if (room.Hotel.AvailableRooms <= 0)
            {
                ModelState.AddModelError("", "No available rooms for this reservation!");
                return View(reservation);
            }

            if (customer == null)
            {
                ModelState.AddModelError("", "Customer not found!");
                return View(reservation);
            }

            reservation.CustomerId = customer.Id;
            reservation.Status = ReservationStatus.Confirmed;
            reservation.RoomId = reservation.RoomId;
            room.Hotel.AvailableRooms = Math.Max(room.Hotel.AvailableRooms - 1, 0); // Rezervasyon yapınca oda sayısı düşme 

            db.Entry(room).State = EntityState.Modified;
            db.Reservations.Add(reservation);
            db.SaveChanges();

            return RedirectToAction("Index", "Hotel");
        }

        ViewBag.RoomId = reservation.RoomId;
        return View(reservation);
    }

    public ActionResult Details(int id)
    {
        bool isAdmin = false;

        if (Session["UserId"] != null)
        {
            var userId = (int)Session["UserId"];
            isAdmin = db.Admins.Any(a => a.Id == userId);
        }

        ViewBag.IsAdmin = isAdmin;

        if (Session["UserId"] != null)
        {
            var userId = (int)Session["UserId"];
            var adminId = db.Admins.FirstOrDefault(a => a.Id == userId);
            var customerId = db.Customers.FirstOrDefault(a => a.Id == userId);
            if (adminId != null)
            {
                ViewBag.AdminName = adminId.FirstName + " " + adminId.LastName;
            }
            if (customerId != null)
            {
                ViewBag.CustomerName = customerId.FirstName + " " + customerId.LastName;
            }
        }

        int customer = (int)Session["UserId"];
        var reservation = db.Reservations
            .Include(r => r.Room)
            .Include(r => r.Room.Hotel)
            .Where(r => r.CustomerId == customer)
            .FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return HttpNotFound();
        }

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
