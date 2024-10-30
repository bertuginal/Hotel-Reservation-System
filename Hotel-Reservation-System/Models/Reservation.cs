using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Check in date cannot be empty!")]
        [DataType(DataType.Date)]
        [DisplayName("Check In Date*")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Check out date cannot be empty!")]
        [DataType(DataType.Date)]
        [DisplayName("Check Out Date*")]
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "Number of guests cannot be empty!")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of guest must be at least 1!")]
        [DisplayName("Number Of Guests*")]
        public int NumberOfGuests { get; set; }

        [Required]
        public ReservationStatus Status { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}