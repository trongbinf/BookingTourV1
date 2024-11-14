using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookingTour.Model
{
    public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public bool Status { get; set; }
		[JsonIgnore]
		public ICollection<Tour> Tours { get; set; } = new List<Tour>();
	}

}
