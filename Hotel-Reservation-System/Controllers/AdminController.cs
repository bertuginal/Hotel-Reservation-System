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
        var admins = db.Admins.ToList();
        return View(admins);
    }

    // GET: Admin/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Admin admin)
    {
        if (ModelState.IsValid)
        {
            admin.CreatedDate = DateTime.Now;
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
                existingAdmin.Email = admin.Email;
                existingAdmin.Password = admin.Password;
                existingAdmin.CreatedDate = admin.CreatedDate;
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
        Admin admin = db.Admins.Find(id);
        if (admin == null)
        {
            return HttpNotFound();
        }
        return View(admin);
    }

    // POST: Admin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Admin admin = db.Admins.Find(id);
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
