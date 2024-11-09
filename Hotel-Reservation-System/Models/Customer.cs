using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class Customer : User
    {
        [Required(ErrorMessage = "Address cannot be empty!")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Address must be at least 10 characters!")]
        [DisplayName("Address*")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must upload your profile photo!")]
        [DisplayName("Profile Picture*")]
        public string ImageUrl { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}