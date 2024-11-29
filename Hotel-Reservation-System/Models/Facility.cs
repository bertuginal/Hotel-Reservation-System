using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hotel_Reservation_System.Models
{
    [Flags]
    public enum Facility
    {
        None = 0,

        [Display(Name = "Bar")]
        Bar = 1,

        [Display(Name = "Free Wi-Fi")]
        FreeWifi = 2,

        [Display(Name = "7/24 Reception")]
        Reception = 4,

        [Display(Name = "Gym")]
        Gym = 8,

        [Display(Name = "Spa")]
        Spa = 16,

        [Display(Name = "Air Conditioning")]
        AirConditioning = 32,

        [Display(Name = "No Smoking")]
        NoSmoking = 64,

        [Display(Name = "Free Breakfast")]
        FreeBreakfast = 128,

        [Display(Name = "Swimming Pool")]
        SwimmingPool = 256,

        [Display(Name = "Pet Friendly")]
        PetFriendly = 512,

        [Display(Name = "Car Charging")]
        ElectricCarCharging = 1024,

        [Display(Name = "Laundry Service")]
        LaundryService = 2048,

        [Display(Name = "Restaurant")]
        Restaurant = 4096,

        [Display(Name = "Beach")]
        Beach = 8192,

        [Display(Name = "Free Parking")]
        FreeParking = 16384
    }

    public static class FacilityIcons
    {
        private static readonly Dictionary<Facility, string> _facilityIcons = new Dictionary<Facility, string>
        {
            { Facility.Bar, "fa fa-glass" },
            { Facility.FreeWifi, "fa fa-wifi" },
            { Facility.Reception, "fa fa-clock" },
            { Facility.Gym, "fa fa-dumbbell" },
            { Facility.Spa, "fa-solid fa-spa" },
            { Facility.AirConditioning, "fa-solid fa-snowflake" },
            { Facility.NoSmoking, "fa-solid fa-ban-smoking" },
            { Facility.FreeBreakfast, "fas fa-hamburger" },
            { Facility.SwimmingPool, "fas fa-swimming-pool" },
            { Facility.PetFriendly, "fa-solid fa-paw" },
            { Facility.ElectricCarCharging, "fa-solid fa-charging-station" },
            { Facility.LaundryService, "fa-solid fa-socks" },
            { Facility.Restaurant, "fa fa-cutlery" },
            { Facility.Beach, "fa-solid fa-umbrella-beach" },
            { Facility.FreeParking, "fa-solid fa-car" }
        };

        public static string GetIcon(Facility facility)
        {
            if (_facilityIcons.TryGetValue(facility, out var icon))
            {
                return icon;
            }
            return string.Empty; // If not found, return empty string
        }
    }

    public static class FacilityExtensions
    {
        public static string GetEnumDisplayName(Facility facility)
        {
            var fieldInfo = facility.GetType().GetField(facility.ToString());
            var displayAttribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                                           .Cast<DisplayAttribute>()
                                           .FirstOrDefault();
            return displayAttribute?.Name ?? facility.ToString();
        }
    }
}