using System;
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
                }
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
                }
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

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                var adminId = db.Admins.FirstOrDefault(a => a.Id == userId);
                if (adminId != null)
                {
                    ViewBag.AdminName = adminId.FirstName + " " + adminId.LastName;
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
