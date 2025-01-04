using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

public class AdminController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Admin
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

        var admins = db.Admins.ToList();
        return View(admins);
    }

    // GET: Admin/Create
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

        return View();
    }

    // POST: Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Admin admin, string ConfirmPassword)
    {
        
        if (ModelState.IsValid)
        {
            if (admin.Password != ConfirmPassword)
            {
                ViewBag.PasswordMismatch = "Passwords do not match!";
                return View(admin);
            }
            admin.CreatedDate = DateTime.Now;
            admin.EditedDate = DateTime.Now;
            db.Admins.Add(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(admin);
    }

    // GET: Admin/Edit/5
    public ActionResult Edit(int id)
    {
        var admin = db.Admins.SingleOrDefault(a => a.Id == id);
        if (admin == null)
        {
            return HttpNotFound();
        }
        bool isAdmin = false;

        if (Session["UserId"] != null)
        {
            var userId = (int)Session["UserId"];
            isAdmin = db.Admins.Any(a => a.Id == userId);
        }

        if (Session["UserId"] != null)
        {
            var userId = (int)Session["UserId"];
            if (admin != null)
            {
                ViewBag.AdminName = admin.FirstName + " " + admin.LastName;
                ViewBag.AdminEmail = admin.Email;

            }
        }

        ViewBag.IsAdmin = isAdmin;

        using (var worldDb = new WorldDbContext())
        {
            var areaCodes = worldDb.AreaCodes.Select(c => c.Name).ToList();
            ViewBag.AreaCodeName = areaCodes;
        }

        return View(admin);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Admin admin)
    {
        if (ModelState.IsValid)
        {
            var existingAdmin = db.Admins.SingleOrDefault(a => a.Id == admin.Id);

            if (existingAdmin != null)
            {
                existingAdmin.FirstName = admin.FirstName;
                existingAdmin.LastName = admin.LastName;
                existingAdmin.Email = existingAdmin.Email;
                existingAdmin.Password = existingAdmin.Password;
                existingAdmin.AreaCode = admin.AreaCode;
                existingAdmin.Phone = admin.Phone;
                existingAdmin.CreatedDate = existingAdmin.CreatedDate;
                existingAdmin.EditedDate = DateTime.Now;

                db.Entry(existingAdmin).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
        return View(admin);
    }



    // GET: Admin/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var admin = db.Admins.Find(id);
        if (admin == null)
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
                ViewBag.AdminEmail = admin.Email;

            }
        }
        return View(admin);
    }

    // POST: Admin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var admin = db.Admins.Find(id);
        if (admin == null)
        {
            return HttpNotFound();
        }

        db.Admins.Remove(admin);
        db.SaveChanges();

        return RedirectToAction("Index");
    }

    // GET: Admin/Details/5
    public ActionResult Details(int id)
    {
        var admin = db.Admins.Find(id);
        if (admin == null)
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
        return View(admin);
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
