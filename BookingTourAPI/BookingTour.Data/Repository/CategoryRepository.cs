using BookingTour.Data.Data;
using BookingTour.Data.Repository.IRepository;
using BookingTour.Model;


namespace BookingTour.Data.Repository
{
	public class CategoryRepository : Repository<Category>, ICategory
	{
		private readonly BookingTourDbContext _db;

		public CategoryRepository(BookingTourDbContext db) : base(db)
		{
			_db = db;
		}


		public async Task UpdateAsync(Category category)
		{
			_db.Categories.Update(category);
		}
	}
}
