using BookingTour.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Model.ViewModel
{
    public class DateStartVm
    {
        public DateOnly StartDate { get; set; }
        public StatusType TypeStatus { get; set; }
        public int TourId { get; set; }
    }
}
