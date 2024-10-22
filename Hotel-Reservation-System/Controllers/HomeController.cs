using Hotel_Reservation_System.DAL;
using Hotel_Reservation_System.Models;
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

            if (Session["UserId"] != null)
            {
                var userId = (int)Session["UserId"];
                var admin = db.Admins.FirstOrDefault(a => a.Id == userId);
                var customer = db.Customers.FirstOrDefault(a => a.Id == userId);
                if (admin != null)
                {
                    ViewBag.AdminName = admin.FirstName + " " + admin.LastName;
                }
                if(customer != null)
                {
                    ViewBag.CustomerName = customer.FirstName + " " + customer.LastName;
                }
            }

            
            var hotels = db.Hotels.ToList();
            return View(hotels);
        }
    }
}