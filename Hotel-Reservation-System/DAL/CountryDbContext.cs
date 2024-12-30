using Hotel_Reservation_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.DAL
{
    public class CountryDbContext : DbContext
    {
        public CountryDbContext() : base("CountryConnection")
        {
        }

        public DbSet<Country> Countries { get; set; }
    }
}