using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hotel_Reservation_System.Models
{
    [Flags]
    public enum FacilityHotel
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


    [Flags]
    public enum FacilityRoom
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
        private static readonly Dictionary<FacilityHotel, string> _facilityHotelIcons = new Dictionary<FacilityHotel, string>
        {
            { FacilityHotel.Bar, "fa fa-glass" },
            { FacilityHotel.FreeWifi, "fa fa-wifi" },
            { FacilityHotel.Reception, "fa fa-clock" },
            { FacilityHotel.Gym, "fa fa-dumbbell" },
            { FacilityHotel.Spa, "fa-solid fa-spa" },
            { FacilityHotel.AirConditioning, "fa-solid fa-snowflake" },
            { FacilityHotel.NoSmoking, "fa-solid fa-ban-smoking" },
            { FacilityHotel.FreeBreakfast, "fas fa-hamburger" },
            { FacilityHotel.SwimmingPool, "fas fa-swimming-pool" },
            { FacilityHotel.PetFriendly, "fa-solid fa-paw" },
            { FacilityHotel.ElectricCarCharging, "fa-solid fa-charging-station" },
            { FacilityHotel.LaundryService, "fa-solid fa-socks" },
            { FacilityHotel.Restaurant, "fa fa-cutlery" },
            { FacilityHotel.Beach, "fa-solid fa-umbrella-beach" },
            { FacilityHotel.FreeParking, "fa-solid fa-car" }
        };

        public static string GetHotelIcon(FacilityHotel facility)
        {
            if (_facilityHotelIcons.TryGetValue(facility, out var icon))
            {
                return icon;
            }
            return string.Empty;
        }

        private static readonly Dictionary<FacilityRoom, string> _facilityRoomIcons = new Dictionary<FacilityRoom, string>
        {
            { FacilityRoom.Bar, "fas fa-tv" },
            { FacilityRoom.FreeWifi, "fa fa-wifi" },
            { FacilityRoom.Reception, "fa fa-clock" },
            { FacilityRoom.Gym, "fa fa-dumbbell" },
            { FacilityRoom.Spa, "fa-solid fa-spa" },
            { FacilityRoom.AirConditioning, "fa-solid fa-snowflake" },
            { FacilityRoom.NoSmoking, "fa-solid fa-ban-smoking" },
            { FacilityRoom.FreeBreakfast, "fas fa-hamburger" },
            { FacilityRoom.SwimmingPool, "fas fa-swimming-pool" },
            { FacilityRoom.PetFriendly, "fa-solid fa-paw" },
            { FacilityRoom.ElectricCarCharging, "fa-solid fa-charging-station" },
            { FacilityRoom.LaundryService, "fa-solid fa-socks" },
            { FacilityRoom.Restaurant, "fa fa-cutlery" },
            { FacilityRoom.Beach, "fa-solid fa-umbrella-beach" },
            { FacilityRoom.FreeParking, "fa-solid fa-car" }
        };

        public static string GetRoomIcon(FacilityRoom facility)
        {
            if (_facilityRoomIcons.TryGetValue(facility, out var icon))
            {
                return icon;
            }
            return string.Empty;
        }
    }

    public static class FacilityExtensions
    {
        public static string GetEnumDisplayNameHotel(FacilityHotel facility)
        {
            var fieldInfo = facility.GetType().GetField(facility.ToString());
            var displayAttribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                                           .Cast<DisplayAttribute>()
                                           .FirstOrDefault();
            return displayAttribute?.Name ?? facility.ToString();
        }

        public static string GetEnumDisplayNameRoom(FacilityRoom facility)
        {
            var fieldInfo = facility.GetType().GetField(facility.ToString());
            var displayAttribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                                           .Cast<DisplayAttribute>()
                                           .FirstOrDefault();
            return displayAttribute?.Name ?? facility.ToString();
        }
    }
}