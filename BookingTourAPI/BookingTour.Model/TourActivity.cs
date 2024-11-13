using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTour.Model
{
    public class TourActivity
    {
        public int TourId {  get; set; }
        public Tour Tour { get; set; }
        public int ActivityId {  get; set; }
        public Activity Activity { get; set; }
    }
}
