using BookingTour.Business.Service.IService;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Activity = BookingTour.Model.Activity;

namespace BookingTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityTourController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityTourController(IActivityService activityService)
        {
            _activityService = activityService;
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddActivityTour([FromBody] ActivityVm activityVm)
        {
            if (activityVm == null)
            {
                return BadRequest("Activity cannot be null.");
            }

            try
            {
                //  Duration
                string durationInput = activityVm.Duration;

                if (!durationInput.Contains(":"))
                {
                    return BadRequest("Invalid duration format. Please use HH:mm.");
                }

                if (durationInput.Count(c => c == ':') == 1 && durationInput.Split(':')[0].Length == 1)
                {
                    durationInput = "0" + durationInput;
                }

                if (!TimeSpan.TryParse(durationInput, out TimeSpan duration))
                {
                    return BadRequest("Invalid duration format. Please use HH:mm.");
                }

                var activity = new Activity
                {
                    ActivityName = activityVm.ActivityName,
                    ActivityType = activityVm.ActivityType,
                    Description = activityVm.Description,
                    Location = activityVm.Location,
                    Duration = duration,
                    TourId = activityVm.TourId


                };

                await _activityService.AddAsync(activity);
                return Ok(new { message = "Add Successfully" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database update error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] ActivityVm activityVm)
        {
            var findding = await _activityService.GetByIdAsync(id);
            if (findding == null)
            {
                return BadRequest("Not Found");
            }

            findding.ActivityName = activityVm.ActivityName;
            findding.ActivityType = activityVm.ActivityType;
            findding.Description = activityVm.Description;
            findding.Location = activityVm.Location;
            findding.Duration = _activityService.Handle(activityVm.Duration);

            await _activityService.UpdateAsync(findding);
            return Ok(findding);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteActitvity(int id)
        {
            var findding = await _activityService.GetByIdAsync(id);
            if (findding == null)
            {
                return BadRequest("Not Found");
            }

            await _activityService.DeleteAsync(id);
            return Ok(findding);
        }



    }
}
