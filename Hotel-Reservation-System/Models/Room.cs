using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Reservation_System.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Room's Image*")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Room number cannot be empty!")]
        [DisplayName("Room's Name*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Room type cannot be empty!")]
        [DisplayName("Room's Type*")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Room's description cannot be empty!")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Room's description must be at least 10 characters!")]
        [DisplayName("Room's Description*")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Room's price per night cannot be empty!")]
        [Range(0, int.MaxValue, ErrorMessage = "Room nightly rate cannot be less than 0!")]
        [DisplayName("Price Per Night*")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Room's capacity cannot be empty!")]
        [Range(1, int.MaxValue, ErrorMessage = "Room capacity must be at least 1!")]
        [DisplayName("Room's Capacity*")]
        public int Capacity { get; set; }

        [DisplayName("Number Of Rooms*")]
        public int NumberOfRooms { get; set; }

        // Foreign key to Hotel
        [Required]
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }


    }
}
