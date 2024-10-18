using Hotel_Reservation_System.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            bool isAdmin = false;
            bool isCustomer = false;

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                isAdmin = db.Admins.Any(a => a.Id == userId);
                isCustomer = db.Customers.Any(a => a.Id == userId);
                if(isAdmin)
                {
                    ViewBag.IsAdmin = isAdmin;
                }
                else if (isCustomer)
                {
                    ViewBag.isCustomer = isCustomer;
                }
            }

            
            var hotels = db.Hotels.ToList();
            return View(hotels);
        }
    }
}