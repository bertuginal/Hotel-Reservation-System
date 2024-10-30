using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class HotelController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    public HotelController()
    {
        db = new ApplicationDbContext();
    }

    // GET: Hotel
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
            }
            if (customerId != null)
            {
                ViewBag.CustomerName = customerId.FirstName + " " + customerId.LastName;
            }
        }

        var hotels = db.Hotels.ToList();
        return View(hotels);
    }

    public ActionResult List()
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

        var hotels = db.Hotels.ToList();
        return View(hotels);
    }

    // GET: Hotel/Create
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

    // POST: Hotel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Hotel hotel, HttpPostedFileBase hotelImage)
    {
        if (ModelState.IsValid)
        {
            if (hotelImage != null && hotelImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(hotelImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                hotelImage.SaveAs(path);

                hotel.ImageUrl = "~/Content/images/" + fileName;
            }

            db.Hotels.Add(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(hotel);
    }

    // GET: Hotel/Edit/5
    public ActionResult Edit(int id)
    {
        var hotel = db.Hotels.Find(id);
        if (hotel == null)
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

        return View(hotel);
    }

    // POST: Hotel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Hotel hotel, HttpPostedFileBase ImageUrl)
    {
        if (ModelState.IsValid)
        {
            if (ImageUrl != null && ImageUrl.ContentLength > 0)
            {
                var fileName = System.IO.Path.GetFileName(ImageUrl.FileName);
                var imagePath = $"~/Content/images/{fileName}";

                string fullPath = Server.MapPath(imagePath);

                try
                {
                    ImageUrl.SaveAs(fullPath);
                    hotel.ImageUrl = imagePath;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Görsel yükleme sırasında hata oluştu: " + ex.Message);
                }
            }
            else
            {
                var existingHotel = db.Hotels.Find(hotel.Id);
                if (existingHotel != null)
                {
                    hotel.ImageUrl = existingHotel.ImageUrl;
                }
            }

            var dbHotel = db.Hotels.Find(hotel.Id);
            if (dbHotel != null)
            {
                dbHotel.Name = hotel.Name;
                dbHotel.Title = hotel.Title;
                dbHotel.Location = hotel.Location;
                dbHotel.Description = hotel.Description;
                dbHotel.Rating = hotel.Rating;
                dbHotel.AvailableRooms = hotel.AvailableRooms;
                dbHotel.PricePerNight = hotel.PricePerNight;
                dbHotel.ImageUrl = hotel.ImageUrl;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Otel bulunamadı.");
            }
        }

        return View(hotel);

    }


    // GET: Hotel/Details/5
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        Hotel hotel = db.Hotels.Find(id);

        if (hotel == null)
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

        return View(hotel);
    }

    // GET: Hotel/Delete/5
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
            }
        }

        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Hotel hotel = db.Hotels.Find(id);
        if (hotel == null)
        {
            return HttpNotFound();
        }
        return View(hotel);
    }

    // POST: Hotel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id)
    {
        var hotel = db.Hotels.Find(id);
        if (hotel != null)
        {
            db.Hotels.Remove(hotel);
            db.SaveChanges();
        }

        return RedirectToAction("List");
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
