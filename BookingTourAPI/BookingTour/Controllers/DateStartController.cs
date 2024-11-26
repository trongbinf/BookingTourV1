using BookingTour.Business.Service;
using BookingTour.Business.Service.IService;
using BookingTour.Model;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateStartController : ControllerBase
    {
        private readonly IDataStartService _dateService;
        private readonly ITourService _tourService;
        public DateStartController(IDataStartService dateService, ITourService tourService)
        {
            _tourService = tourService;
            _dateService = dateService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDate(int idTour, [FromBody] DateStartVm[] dateStartVms)
        {
            var findding = await _tourService.GetByIdAsync(idTour);
            if (findding == null || dateStartVms == null || dateStartVms.Length == 0)
            {
                return BadRequest("Tour not found or no dates provided.");
            }

            var dateStarts = new List<DateStart>();

            foreach (var dateStartVm in dateStartVms)
            {
                DateOnly startDate = new DateOnly(dateStartVm.StartDate.Year, dateStartVm.StartDate.Month, dateStartVm.StartDate.Day);
                var dateStart = new DateStart
                {
                    StartDate = startDate,
                    TypeStatus = dateStartVm.TypeStatus,
                    TourId = idTour
                };

                dateStarts.Add(dateStart);
            }

            try
            {
                foreach (var item in dateStarts)
                {
                    await _dateService.AddAsync(item);
                }
                return Ok(new { message = "Add Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDate(int idDate, [FromBody] DateStartVm dateStartVm)
        {
            var findding = await _dateService.GetByIdAsync(idDate);
            if(findding == null)
            {
                return BadRequest("Not Found");
            }

            findding.StartDate = dateStartVm.StartDate;
            findding.TypeStatus = dateStartVm.TypeStatus;
            await _dateService.UpdateAsync(findding);
            return Ok(findding);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDateStart(int id)
        {
            var findding = await _dateService.GetByIdAsync(id);
            if (findding == null)
            {
                return BadRequest("Not Found");
            }

            await _dateService.DeleteAsync(id);
            return Ok(findding);
        }
    }
}
