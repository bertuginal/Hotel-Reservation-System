using System.Linq;
using System.Net;
using System.Web.Mvc;
using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
using System.Web.Security;


public class AccountController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Account/RegisterAdmin
    public ActionResult RegisterAdmin()
    {
        return View();
    }

    // POST: Account/RegisterAdmin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult RegisterAdmin(Admin admin)
    {
        if (ModelState.IsValid)
        {
            db.Admins.Add(admin);
            admin.CreatedDate = System.DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        return View(admin);
    }

    // GET: Account/RegisterCustomer
    public ActionResult RegisterCustomer()
    {
        return View();
    }

    // POST: Account/RegisterCustomer
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult RegisterCustomer(Customer customer)
    {
        if (ModelState.IsValid)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("LoginCustomer");
        }
        return View(customer);
    }

    // GET: Account/LoginAdmin
    public ActionResult LoginAdmin()
    {
        return View();
    }

    // POST: Account/LoginAdmin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LoginAdmin(string email, string password)
    {
            var admin = db.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);
            if (admin != null)
            {
                Session["UserId"] = admin.Id;
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("", "Invalid e-mail or password!");
            return View();
    }


    // GET: Account/LoginCustomer
    public ActionResult LoginCustomer()
    {
        return View();
    }

    // POST: Account/LoginCustomer
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LoginCustomer(string email, string password)
    {
        if (ModelState.IsValid)
        {
            var customer = db.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);
            if (customer != null)
            {
                Session["UserId"] = customer.Id;
                return RedirectToAction("Index", "Hotel");
            }else {
                ModelState.AddModelError("", "Invalid e-mail or password!");
            }
            
        }
        return View();
    }

    public ActionResult Logout()
    {
        Session.Clear();
        FormsAuthentication.SignOut();
        return RedirectToAction("Index", "Home");
    }
}
