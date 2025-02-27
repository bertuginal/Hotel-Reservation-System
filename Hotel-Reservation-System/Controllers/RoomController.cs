﻿using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Hotel_Reservation_System.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext db;

        public RoomController()
        {
            db = new ApplicationDbContext();
        }

        private void UpdateAvailableRooms(int hotelId)
        {
            var hotel = db.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Id == hotelId);
            if (hotel != null)
            {
                hotel.AvailableRooms = hotel.Rooms.Sum(r => r.NumberOfRooms);
                db.SaveChanges();
            }
        }

        // GET: Room
        public ActionResult Index()
        {
            bool isAdmin = false;
            bool isCustomer = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];

                isAdmin = db.Admins.Any(a => a.Id == userId);
                isCustomer = db.Customers.Any(a => a.Id == userId);
                if (isAdmin)
                {
                    ViewBag.IsAdmin = isAdmin;
                }
                else if (isCustomer)
                {
                    ViewBag.isCustomer = isCustomer;
                }
            }

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                var admin = db.Admins.FirstOrDefault(a => a.Id == userId);
                var customerId = db.Customers.FirstOrDefault(a => a.Id == userId);
                if (admin != null)
                {
                    ViewBag.AdminName = admin.FirstName + " " + admin.LastName;
                    ViewBag.AdminEmail = admin.Email;

                }
                if (customerId != null)
                {
                    ViewBag.CustomerName = customerId.FirstName + " " + customerId.LastName;
                    ViewBag.CustomerEmail = customerId.Email;

                }
            }

            var rooms = db.Rooms.Include(r => r.Hotel).ToList();
            return View(rooms);
        }

        // GET: Room/Details/5
        public ActionResult Details(int? id)
        {
            bool isAdmin = false;
            bool isCustomer = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];

                isAdmin = db.Admins.Any(a => a.Id == userId);
                isCustomer = db.Customers.Any(a => a.Id == userId);
                if (isAdmin)
                {
                    ViewBag.IsAdmin = isAdmin;
                }
                else if (isCustomer)
                {
                    ViewBag.isCustomer = isCustomer;
                }
            }

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                var admin = db.Admins.FirstOrDefault(a => a.Id == userId);
                var customerId = db.Customers.FirstOrDefault(a => a.Id == userId);
                if (admin != null)
                {
                    ViewBag.AdminName = admin.FirstName + " " + admin.LastName;
                    ViewBag.AdminEmail = admin.Email;

                }
                if (customerId != null)
                {
                    ViewBag.CustomerName = customerId.FirstName + " " + customerId.LastName;
                    ViewBag.CustomerEmail = customerId.Email;

                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Room room = db.Rooms.Include(r => r.Hotel).FirstOrDefault(r => r.Id == id); // Belirtilen oda ve ona bağlı otel

            if (room == null)
            {
                return HttpNotFound();
            }

            return View(room);
        }

        // GET: Room/Create
        public ActionResult Create()
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
                if (adminId != null)
                {
                    ViewBag.AdminName = adminId.FirstName + " " + adminId.LastName;
                    ViewBag.AdminEmail = adminId.Email;

                }
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name");
            ViewBag.RoomTypes = new SelectList(new List<string> {"Standard Room", "Family Room", "Suite Room" });

            ViewBag.Facilities = Enum.GetValues(typeof(FacilityRoom)).Cast<FacilityRoom>().ToList();

            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room, HttpPostedFileBase roomImage, int[] SelectedFacilities)
        {
            if (ModelState.IsValid)
            {
                if (roomImage != null && roomImage.ContentLength > 0)
                {
                        var fileName = Path.GetFileName(roomImage.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images/room-images"), fileName);
                        roomImage.SaveAs(path);

                        room.ImageUrl = "~/Content/images/room-images/" + fileName;
                }
                if (SelectedFacilities != null && SelectedFacilities.Any())
                {
                    room.Facilities = SelectedFacilities.Aggregate(FacilityRoom.None, (current, value) => current | (FacilityRoom)value);
                }
                else
                {
                    room.Facilities = FacilityRoom.None;
                }
                db.Rooms.Add(room);
                db.SaveChanges();
                UpdateAvailableRooms(room.HotelId);
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", selectedValue: null);
            ViewBag.HotelIdList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select a Hotel", Value = "" }
            }.Concat(db.Hotels.Select(h => new SelectListItem
            {
                Text = h.Name,
                Value = h.Id.ToString()
            })).ToList();

            ViewBag.RoomTypes = new SelectList(new List<string> { "Standard Room", "Family Room", "Suite Room" });

            ViewBag.Facilities = Enum.GetValues(typeof(FacilityRoom)).Cast<FacilityRoom>().ToList();

            return View(room);
        }

        // GET: Room/Edit/5
    public ActionResult Edit(int id)
    {
        var room = db.Rooms.Find(id);
            bool isAdmin = false;
            bool isCustomer = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];

                isAdmin = db.Admins.Any(a => a.Id == userId);
                isCustomer = db.Customers.Any(a => a.Id == userId);
                if (isAdmin)
                {
                    ViewBag.IsAdmin = isAdmin;
                }
                else if (isCustomer)
                {
                    ViewBag.isCustomer = isCustomer;
                }
            }

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                var admin = db.Admins.FirstOrDefault(a => a.Id == userId);
                var customerId = db.Customers.FirstOrDefault(a => a.Id == userId);
                if (admin != null)
                {
                    ViewBag.AdminName = admin.FirstName + " " + admin.LastName;
                    ViewBag.AdminEmail = admin.Email;

                }
                if (customerId != null)
                {
                    ViewBag.CustomerName = customerId.FirstName + " " + customerId.LastName;
                    ViewBag.CustomerEmail = customerId.Email;

                }
            }

            if (room == null)
        {
            return HttpNotFound();
        }

        ViewBag.Hotels = new SelectList(db.Hotels, "Id", "Name", room.HotelId);

        ViewBag.AllFacilities = Enum.GetValues(typeof(FacilityRoom)).Cast<FacilityRoom>()
        .Where(f => f != FacilityRoom.None).ToList();

        return View(room);
    }

        // POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room model, HttpPostedFileBase ImageFile, int[] SelectedFacilities)
        {
            if (ModelState.IsValid)
            {
                var existingRoom = db.Rooms.Find(model.Id);
                if (existingRoom != null)
                {
                    existingRoom.Name = model.Name;
                    existingRoom.Type = model.Type;
                    existingRoom.HotelId = model.HotelId;
                    existingRoom.Capacity = model.Capacity;
                    existingRoom.SquareMeters = model.SquareMeters;
                    existingRoom.NumberOfRooms = model.NumberOfRooms;
                    existingRoom.PricePerNight = model.PricePerNight;
                    existingRoom.Description = model.Description;

                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(ImageFile.FileName);
                        var directoryPath = Server.MapPath("~/Content/room-images");

                        // Dizin yoksa oluştur
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        var path = Path.Combine(directoryPath, fileName);
                        ImageFile.SaveAs(path);
                        existingRoom.ImageUrl = "~/Content/room-images/" + fileName; // URL'yi güncelle
                    }

                    if (SelectedFacilities != null && SelectedFacilities.Any())
                    {
                        existingRoom.Facilities = SelectedFacilities.Aggregate(FacilityRoom.None, (current, facility) => current | (FacilityRoom)facility);
                    }
                    else
                    {
                        existingRoom.Facilities = FacilityRoom.None;
                    }

                    db.SaveChanges();
                    UpdateAvailableRooms(model.HotelId);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Hotels = new SelectList(db.Hotels, "Id", "Name", model.HotelId);

            ViewBag.AllFacilities = Enum.GetValues(typeof(FacilityRoom)).Cast<FacilityRoom>()
            .Where(f => f != FacilityRoom.None).ToList();

            return View(model);
        }


        // GET: Room/Delete/5
        public ActionResult Delete(int? id)
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
                if (adminId != null)
                {
                    ViewBag.AdminName = adminId.FirstName + " " + adminId.LastName;
                    ViewBag.AdminEmail = adminId.Email;

                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var room = db.Rooms.Include(r => r.Hotel).FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return HttpNotFound();
            }

            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var room = db.Rooms.Find(id);
            var hotel = db.Hotels.Find(room.HotelId);

            if (room == null)
            {
                return HttpNotFound();
            }
            if (room != null)
            {
                hotel.AvailableRooms -= room.NumberOfRooms;
                if (hotel.AvailableRooms < 0)
                {
                    hotel.AvailableRooms = 0;
                }
            }

            db.Rooms.Remove(room);
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
