using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name cannot be empty!")]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters!")]
        [DisplayName("First Name*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty!")]
        [MinLength(3, ErrorMessage = "Last name must be at least 3 characters!")]
        [DisplayName("Last Name*")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail cannot be empty!")]
        [EmailAddress(ErrorMessage = "Your email address is not available!")]
        [DisplayName("E-mail*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters!")]
        [DataType(DataType.Password)]
        [DisplayName("Password*")]
        public string Password { get; set; }
        public DateTime EditedDate { get; set; }

        [Required(ErrorMessage = "Phone number cannot be empty!")]
        [Phone(ErrorMessage = "Please enter a valid phone number!")]
        [DisplayName("Phone Number*")]
        public string Phone { get; set; }


    }
}