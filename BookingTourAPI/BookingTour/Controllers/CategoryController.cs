using BookingTour.Data.Repository;
using BookingTour.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategory()
        {
            var list = await _unitOfWork.Category.GetAllAsync();
            list = list.Where(c => c.Status == true).ToList();
            return Ok(list);
        }
    }
}
