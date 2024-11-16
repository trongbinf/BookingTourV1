﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;


namespace BookingTour.Model
{
    public class Tour
	{
		public int TourId { get; set; }
		public string TourName { get; set; }
		public string Description { get; set; }
		public string? MainImage {  get; set; }
		public string? OtherImage {  get; set; }
		public string City { get; set; }
        public string Country { get; set; }
        public bool IsFullDay { get; set; }
		public double Price { get; set; }
		public string VehicleType {  get; set; }
		public DateTime Created { get; set; } = DateTime.Now;
        public bool Status { get; set; }
		
		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
		[JsonIgnore]
		public  ICollection<DateStart> DateStarts { get; set; }
		[JsonIgnore]
		public  ICollection<Activity> Activities { get; set; } 
		[JsonIgnore]
		public  ICollection<Booking> Bookings { get; set; }
		[JsonIgnore]
		public  ICollection<Review> Reviews { get; set; }


		
	}
	
}
