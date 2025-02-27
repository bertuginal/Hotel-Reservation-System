﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.DAL;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Security.Claims;

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

            var customers = db.Customers.ToList();
            return View(customers);
        }

        // GET: Customer/Settings
        public ActionResult Settings()
        {
            int customerId = (int)Session["UserId"];
            var customer = db.Customers.Find(customerId);
            bool isCustomer = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isCustomer = db.Customers.Any(a => a.Id == userId);
            }

            ViewBag.IsCustomer = isCustomer;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                var customerID = db.Customers.FirstOrDefault(a => a.Id == userId);
                if (customerID != null)
                {
                    ViewBag.CustomerName = customerID.FirstName + " " + customerID.LastName;
                    ViewBag.CustomerEmail = customerID.Email;

                }
            }

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
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

            using (var worldDb = new WorldDbContext())
            {
                var countries = worldDb.Countries.Select(c => c.Name).ToList();
                ViewBag.CountryName = countries;

                var areaCodes = worldDb.AreaCodes.Select(c => c.Name).ToList();
                ViewBag.AreaCodeName = areaCodes;
            }

            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer, string ConfirmPassword, HttpPostedFileBase profilePicture)
        {
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                if (customer.Password != ConfirmPassword)
                {
                    ViewBag.PasswordMismatch = "Passwords do not match!";
                    return View(customer);
                }

                if (profilePicture != null && profilePicture.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(profilePicture.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/profile-photos"), fileName);
                    profilePicture.SaveAs(path);

                    customer.ImageUrl = "~/Content/images/profile-photos/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("ImageUrl", "You must upload your profile photo!");
                    return View(customer);
                }

                customer.EditedDate = DateTime.Now;
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

            using (var worldDb = new WorldDbContext())
            {
                var countries = worldDb.Countries.Select(c => c.Name).ToList();
                ViewBag.CountryName = countries;

                var areaCodes = worldDb.AreaCodes.Select(c => c.Name).ToList();
                ViewBag.AreaCodeName = areaCodes;

            }

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer, HttpPostedFileBase profilePicture)
        {
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                var existingCustomer = db.Customers.SingleOrDefault(a => a.Id == customer.Id);

                if (existingCustomer == null)
                {
                    return HttpNotFound();
                }

                if (profilePicture != null && profilePicture.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(profilePicture.FileName);
                    var imagePath = $"~/Content/images/profile-photos/{fileName}";
                    string fullPath = Server.MapPath(imagePath);

                    try
                    {
                        profilePicture.SaveAs(fullPath);
                        existingCustomer.ImageUrl = imagePath;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Görsel yükleme sırasında hata oluştu: " + ex.Message);
                        return View(customer);
                    }
                }
                else
                {
                    customer.ImageUrl = existingCustomer.ImageUrl;
                }

                if (existingCustomer != null)
                {
                    existingCustomer.FirstName = customer.FirstName;
                    existingCustomer.LastName = customer.LastName;
                    existingCustomer.Email = existingCustomer.Email;
                    existingCustomer.Password = existingCustomer.Password;
                    existingCustomer.AreaCode = customer.AreaCode;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Country = customer.Country;
                    existingCustomer.City = customer.City;
                    existingCustomer.PostalCode = customer.PostalCode;
                    existingCustomer.EditedDate = DateTime.Now;

                    db.Entry(existingCustomer).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            using (var worldDb = new WorldDbContext())
            {
                var countries = worldDb.Countries.Select(c => c.Name).ToList();
                ViewBag.CountryName = new SelectList(countries, customer.Country);
            }

            return View(customer);
        }


        // GET: Customer/Update/5
        public ActionResult Update(int? id)
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

            bool isCustomer = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isCustomer = db.Customers.Any(a => a.Id == userId);
            }

            ViewBag.IsCustomer = isCustomer;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                var customerId = db.Customers.FirstOrDefault(a => a.Id == userId);
                if (customerId != null)
                {
                    ViewBag.CustomerName = customerId.FirstName + " " + customerId.LastName;
                    ViewBag.CustomerEmail = customerId.Email;

                }
            }

            ViewBag.ImagePreviewUrl = !string.IsNullOrEmpty(customer.ImageUrl)
                ? System.IO.Path.GetFileName(customer.ImageUrl)
                : null;

            using (var worldDb = new WorldDbContext())
            {
                var countries = worldDb.Countries.Select(c => c.Name).ToList();
                ViewBag.CountryName = countries;

                var areaCodes = worldDb.AreaCodes.Select(c => c.Name).ToList();
                ViewBag.AreaCodeName = areaCodes;
            }

            return View(customer);
        }

        // POST: Customer/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Customer customer, HttpPostedFileBase profilePicture)
        {
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                var existingCustomer = db.Customers.SingleOrDefault(a => a.Id == customer.Id);

                if (existingCustomer == null)
                {
                    return HttpNotFound();
                }

                if (profilePicture != null && profilePicture.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(profilePicture.FileName);
                    var imagePath = $"~/Content/images/profile-photos/{fileName}";
                    string fullPath = Server.MapPath(imagePath);

                    try
                    {
                        profilePicture.SaveAs(fullPath);
                        existingCustomer.ImageUrl = imagePath;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Görsel yükleme sırasında hata oluştu: " + ex.Message);
                        return View(customer);
                    }
                }
                else
                {
                    customer.ImageUrl = existingCustomer.ImageUrl;
                }

                if (existingCustomer != null)
                {
                    existingCustomer.FirstName = customer.FirstName;
                    existingCustomer.LastName = customer.LastName;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Password = customer.Password;
                    existingCustomer.AreaCode = customer.AreaCode;
                    existingCustomer.Phone = customer.Phone;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Country = customer.Country;
                    existingCustomer.City = customer.City;
                    existingCustomer.PostalCode = customer.PostalCode;
                    existingCustomer.EditedDate = DateTime.Now;

                    db.Entry(existingCustomer).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Settings");
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


        [HttpPost]
        public ActionResult UploadProfilePicture(int customerId, HttpPostedFileBase profilePicture)
        {
            if (profilePicture == null || profilePicture.ContentLength == 0)
            {
                return Json(new { success = false, errorMessage = "No file selected." });
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(profilePicture.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return Json(new { success = false, errorMessage = "Invalid file type. Please upload PNG or JPEG." });
            }

            if (profilePicture.ContentLength > 15 * 1024 * 1024)
            {
                return Json(new { success = false, errorMessage = "File size exceeds the maximum allowed size of 15MB." });
            }

            try
            {
                // Save the file to the server
                var fileName = Path.GetFileName(profilePicture.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/images/profile-photos"), fileName);
                profilePicture.SaveAs(filePath);

                // Update the customer's ImageUrl in the database
                var customer = db.Customers.Find(customerId);
                if (customer != null)
                {
                    customer.ImageUrl = "~/Content/images/profile-photos/" + fileName;
                    db.SaveChanges();
                }

                // Return the new image URL
                return Json(new { success = true, newImageUrl = Url.Content("~/Content/images/profile-photos/" + fileName) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "An error occurred while uploading the image: " + ex.Message });
            }
        }


        [HttpPost]
        public ActionResult DeleteProfilePicture(int customerId)
        {
            var customer = db.Customers.Find(customerId);

            if (customer != null)
            {
                customer.ImageUrl = "~/Content/images/profile-photos/default.jpg";

                db.SaveChanges();
            }

            return Json(new { success = true, imageUrl = customer?.ImageUrl });
        }



        public ActionResult Wishlist()
        {
            var userId = Session["UserId"] != null ? (int)Session["UserId"] : 0;
            var user = db.Users.Include(u => u.WishlistedHotels).FirstOrDefault(u => u.Id == userId);
            bool isAdmin = false;
            bool isCustomer = false;

            if (userId == 0)
            {
                return RedirectToAction("LoginCustomer", "Account");
            }

            if (Session["UserId"] != null)
            {
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

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user.WishlistedHotels);
        }


        public ActionResult ToggleWishlist(int id, string referer)
        {
            var userId = Session["UserId"] != null ? (int)Session["UserId"] : 0;

            if (userId == 0)
            {
                return RedirectToAction("LoginCustomer", "Account");
            }

            var user = db.Users.Include(u => u.WishlistedHotels).FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToAction("LoginCustomer", "Account");
            }

            var hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }

            if (user.WishlistedHotels.Contains(hotel))
            {
                user.WishlistedHotels.Remove(hotel);
            }
            else
            {
                user.WishlistedHotels.Add(hotel);
            }

            db.SaveChanges();

            if (!string.IsNullOrEmpty(referer) && referer.Contains("Wishlist"))
            {
                return RedirectToAction("Wishlist", "Customer");
            }
            else if (!string.IsNullOrEmpty(referer) && referer.Contains("Hotel/Details"))
            {
                return RedirectToAction("Details", "Hotel", new { id = hotel.Id });
            }
            else if (!string.IsNullOrEmpty(referer) && referer.Contains("Hotel"))
            {
                return RedirectToAction("Index", "Hotel");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public JsonResult GetCitiesByCountry(string countryName)
        {
            using (var worldDb = new WorldDbContext())
            {
                // Ülke adına göre ID al
                var countryId = worldDb.Countries
                    .Where(c => c.Name.Equals(countryName, StringComparison.OrdinalIgnoreCase))
                    .Select(c => c.Id)
                    .FirstOrDefault();

                // Eğer ülke bulunamazsa boş bir liste döndür
                if (countryId == 0)
                {
                    return Json(new List<string>(), JsonRequestBehavior.AllowGet);
                }

                // Şehirleri al
                var cities = worldDb.Cities
                    .Where(c => c.CountryId == countryId)
                    .Select(c => c.Name)
                    .ToList();

                return Json(cities, JsonRequestBehavior.AllowGet);
            }
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
