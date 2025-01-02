using Hotel_Reservation_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.DAL
{
    public class WorldDbContext : DbContext
    {
        public WorldDbContext() : base("WorldConnection")
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}