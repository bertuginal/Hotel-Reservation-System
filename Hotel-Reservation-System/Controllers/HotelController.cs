using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

public class HotelController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

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
    public ActionResult Edit(int? id)
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
        return View(hotel);
    }

    // POST: Hotel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            db.Entry(hotel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
    public ActionResult DeleteConfirmed(int id)
    {
        Hotel hotel = db.Hotels.Find(id);
        db.Hotels.Remove(hotel);
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
