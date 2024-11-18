using BookingTour.Business.Service;
using BookingTour.Business.Service.IService;
using BookingTour.Data.Data;
using BookingTour.Model;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rm = BookingTour.Business.Service.HandleTextUnicode;

namespace BookingTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BookingTourDbContext _context;

        public UserController(UserManager<AppUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                BookingTourDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {

            var usersWithRoles = await _context.Users
                .Select(user => new AppUserVm
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = _context.UserRoles
                    .Where(ur => ur.UserId == user.Id)
                                .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                                .FirstOrDefault(),
                    Status = user.LockoutEnabled,
                    Bookings = _context.Bookings
                                .Where(b => b.UserId == user.Id)
                                .Select(b => new UserBooking
                                {
                                    BookingId = b.BookingId,
                                    BookingDate = b.BookingDate,
                                    Status = b.Status,
                                    Notes = b.Notes
                                })
                                .ToList()
                })
                .ToListAsync();

            return Ok(usersWithRoles);
        }

        [HttpPost("set-role-user/{userId}")]
        public async Task<IActionResult> SetUserRole(string userId, string roleName)
        {
            // Tìm người dùng dựa trên userId
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Kiểm tra xem vai trò có tồn tại không
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return BadRequest(new { Message = "Role does not exist" });
            }

            // Lấy danh sách các vai trò của người dùng
            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count > 0)
            {
                // Xóa tất cả vai trò hiện tại của người dùng
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                {
                    return BadRequest(new { Message = "Failed to remove existing roles", Errors = removeResult.Errors });
                }
            }

            // Thêm vai trò mới cho người dùng
            var addResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!addResult.Succeeded)
            {
                return BadRequest(new { Message = "Failed to add role", Errors = addResult.Errors });
            }

            return Ok(new { Message = $"Role '{roleName}' assigned to user '{user.UserName}' successfully" });
        }

        [HttpPost("block-user/{userId}")]
        public async Task<IActionResult> BlockUser(string userId)
        {
            // Tìm người dùng dựa trên userId
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.LockoutEnabled = !user.LockoutEnabled;

            // Đặt thời gian khóa cho người dùng (ví dụ: 30 ngày từ thời điểm hiện tại)
            //user.LockoutEnd = DateTimeOffset.UtcNow.AddDays(30);


            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Failed to block user", Errors = result.Errors });
            }

            return Ok(new { Message = $"User '{user.UserName}' has been blocked until {user.LockoutEnd}" });
        }

        [HttpGet("search-user")]
        public async Task<ActionResult<PaginatedList<AppUserVm>>> SearchUser(
                      [FromQuery] string? key = null,
                      [FromQuery] int pageIndex = 1,
                      [FromQuery] int pageSize = 5)
        {
            // Perform join operation first to get users with roles
            var usersWithRoles = await _context.Users
                .Select(user => new AppUserVm
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = _context.UserRoles
                        .Where(ur => ur.UserId == user.Id)
                        .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                        .FirstOrDefault(),
                    Status = user.LockoutEnabled,
                    Bookings = _context.Bookings
                       .Where(b => b.UserId == user.Id)
                        .Select(b => new UserBooking
                        {
                            BookingId = b.BookingId,
                            BookingDate = b.BookingDate,
                            Status = b.Status,
                            Notes = b.Notes
                        })
                       .ToList()
                })
                .ToListAsync();

            // Filter the results if a key is provided
            if (!string.IsNullOrEmpty(key))
            {
                var normalizedKey = rm.RemoveUnicode(key);
                usersWithRoles = usersWithRoles
                    .Where(user =>
                        rm.RemoveUnicode(user.UserName).Contains(normalizedKey, StringComparison.OrdinalIgnoreCase) ||
                        rm.RemoveUnicode(user.Email).Contains(normalizedKey, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Get the total count for pagination
            int totalCount = usersWithRoles.Count();

            // Apply pagination
            var paginatedUsers = usersWithRoles
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Create paginated response
            var response = new PaginatedList<AppUserVm>(paginatedUsers, totalCount, pageIndex, pageSize);
            return Ok(response);
        }

    }
}