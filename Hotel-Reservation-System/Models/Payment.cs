using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class Payment
    {
        public int Id { get; set; }

        // Foreign key for Reservation
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

        [Required(ErrorMessage = "First name cannot be empty!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Card number cannot be empty!")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration date cannot be empty!")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "CVV cannot be empty!")]
        public string CVV { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }

}