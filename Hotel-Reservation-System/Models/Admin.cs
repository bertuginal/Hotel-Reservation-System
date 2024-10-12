using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class Admin : User
    {
        public DateTime CreatedDate { get; set; }
    }
}