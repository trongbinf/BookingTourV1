using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Model.ViewModel
{
    public class TourAddVm
    {
        public int? TourId { get; set; }
        public string TourName { get; set; }
        public string Description { get; set; }
        public IFormFile? MainImage { get; set; }
        public List<IFormFile>? DetailImages { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public bool IsFullDay { get; set; }
        public double Price { get; set; }
        public int PersonNumber { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<DateStart>? DateStarts { get; set; }
        public IEnumerable<Activity>? Activities { get; set; }
        public IEnumerable<Booking>? Bookings { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public Category? Category { get; set; }

    }
}
