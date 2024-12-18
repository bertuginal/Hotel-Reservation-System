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

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string ExpirationDate { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }

}