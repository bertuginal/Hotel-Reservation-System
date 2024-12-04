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

        //Bathroom Facilities
        [Display(Name = "Hair Dryer")]
        HairDryer = 1,

        [Display(Name = "Private Bathroom ")]
        PrivateBathroom = 2,

        [Display(Name = "Shampoo")]
        Shampoo = 4,

        [Display(Name = "Toilet Paper")]
        ToiletPaper = 8,

        [Display(Name = "Towel")]
        Towel = 16,

        //Bedroom Facilities
        [Display(Name = "Air Conditioning")]
        AirConditioning = 32,

        [Display(Name = "Sheet Set")]
        SheetSet = 64,

        [Display(Name = "Light/noise/heat proof curtains")]
        Curtains = 128,

        [Display(Name = "Room Safe")]
        RoomSafe = 256,

        [Display(Name = "Ironing Board")]
        IroningBoard = 512,

        //Saloon Facilities
        [Display(Name = "80 inch Smart TV")]
        SmartTV = 1024,

        [Display(Name = "Seating Group")]
        SeatingGroup = 2048,

        [Display(Name = "Balcony")]
        Balcony = 4096,

        [Display(Name = "Mini Fridge")]
        MiniFridge = 8192,

        [Display(Name = "Electric Kettle")]
        ElectricKettle = 16384
    }
    public static class RoomFacilityGroups
    {
        public static readonly FacilityRoom BathroomFacilities = FacilityRoom.HairDryer | FacilityRoom.PrivateBathroom | FacilityRoom.Shampoo | FacilityRoom.ToiletPaper | FacilityRoom.Towel;
        public static readonly FacilityRoom BedroomFacilities = FacilityRoom.AirConditioning | FacilityRoom.SheetSet | FacilityRoom.Curtains | FacilityRoom.RoomSafe | FacilityRoom.IroningBoard;
        public static readonly FacilityRoom SaloonFacilities = FacilityRoom.SmartTV | FacilityRoom.SeatingGroup | FacilityRoom.Balcony | FacilityRoom.MiniFridge | FacilityRoom.ElectricKettle;
    }

    public static class FacilityIcons
    {
        //Hotel Facilities
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

        //Room Facilities
        private static readonly Dictionary<FacilityRoom, string> _facilityRoomIcons = new Dictionary<FacilityRoom, string>
        {
            { FacilityRoom.HairDryer, "fa-solid fa-check" },
            { FacilityRoom.PrivateBathroom, "fa-solid fa-check" },
            { FacilityRoom.Shampoo, "fa-solid fa-check" },
            { FacilityRoom.ToiletPaper, "fa-solid fa-check" },
            { FacilityRoom.Towel, "fa-solid fa-check" },
            { FacilityRoom.AirConditioning, "fa-solid fa-check" },
            { FacilityRoom.SheetSet, "fa-solid fa-check" },
            { FacilityRoom.Curtains, "fa-solid fa-check" },
            { FacilityRoom.RoomSafe, "fa-solid fa-check" },
            { FacilityRoom.IroningBoard, "fa-solid fa-check" },
            { FacilityRoom.SmartTV, "fa-solid fa-check" },
            { FacilityRoom.SeatingGroup, "fa-solid fa-check" },
            { FacilityRoom.Balcony, "fa-solid fa-check" },
            { FacilityRoom.MiniFridge, "fa-solid fa-check" },
            { FacilityRoom.ElectricKettle, "fa-solid fa-check" }

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