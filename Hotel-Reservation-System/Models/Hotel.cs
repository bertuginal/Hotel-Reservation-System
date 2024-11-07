using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Hotel_Reservation_System.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Hotel's Image*")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Hotel's name cannot be empty!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Hotel's name must be at least 3 characters!")]
        [DisplayName("Hotel's Name*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hotel's title cannot be empty!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Hotel's title must be at least 50 characters!")]
        [DisplayName("Hotel's Title*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Hotel's location cannot be empty!")]
        [DisplayName("Location*")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Hotel's location cannot be empty!")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "Hotel's description must be at least 10 and maximum 5000 characters!")]
        [DisplayName("Features*")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Hotel's rating cannot be empty!")]
        [Range(1, 5)]
        [DisplayName("Rating* (1 to 5)")]
        public int Rating { get; set; }


        [DisplayName("Number Of Rooms Available*")]
        public int AvailableRooms { get; set; }


        [Required(ErrorMessage = "Please enter the hotel price per night!")]
        [Range(0, int.MaxValue, ErrorMessage = "Hotel nightly rate cannot be less than 0!")]
        [DisplayName("Price Per Night*")]
        public decimal PricePerNight { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }


    }
}