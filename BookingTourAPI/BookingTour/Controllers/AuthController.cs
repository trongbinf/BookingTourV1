using Azure;
using BookingTour.Business.Service.IService;
using BookingTour.Data.Data;
using BookingTour.Model;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BookingTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BookingTourDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            , BookingTourDbContext context,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVm registerVm)
        {
            // Kiểm tra xem người dùng đã tồn tại chưa
            var userExists = await _userManager.FindByEmailAsync(registerVm.Email);
            if (userExists != null)
            {
                if (userExists.EmailConfirmed)
                {
                    return BadRequest($"User {registerVm.Email} already exists!");
                }
                else
                {
                    // Tạo token xác thực email cho người dùng chưa xác thực
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(userExists);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = registerVm.Email }, Request.Scheme);

                    // Gửi lại email xác thực
                    var message = new Message(new string[] { userExists.Email! }, "Confirmation email link", confirmationLink!);
                    _emailService.SendEmail(message);

                    // Thông báo rằng tài khoản đã tồn tại nhưng cần xác thực email
                    return Ok(new { message = $"Confirmation email has been resent to {registerVm.Email}. Please confirm your email to activate your account." });
                }
            }

            // Tạo mới người dùng
            var user = new AppUser
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                

            };

            // Tạo người dùng nhưng không cho phép đăng nhập cho đến khi xác thực email
            var result = await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User could not be created" });
            }

            // Tạo token xác thực email cho người dùng mới
            var newToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var newConfirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token = newToken, email = registerVm.Email }, Request.Scheme);

            // Gửi email xác thực
            var newMessage = new Message(new string[] { user.Email! }, "Confirmation email link", newConfirmationLink!);
            _emailService.SendEmail(newMessage);

            // Thông báo rằng người dùng đã được tạo nhưng cần xác thực email
            return Created(nameof(Register), new { message = $"User {registerVm.Email} created! Please confirm your email to activate your account." });
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Invalid email confirmation request.");
            }

            // Xác thực email với token
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                // Gán vai trò cho người dùng nếu cần
                await _userManager.AddToRoleAsync(user, "User");

                // Chuyển hướng người dùng đến trang đăng nhập Angular sau khi xác thực thành công
                return Redirect($"http://localhost:4200/register?token={token}");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Email confirmation failed." });
            }
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "All fields are required!" });
            }

            var user = await _userManager.FindByEmailAsync(loginVm.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginVm.Password) && user.EmailConfirmed && user.LockoutEnabled)
            {
                var tokenValue = await GenerateJWTToken(user);
                return Ok(tokenValue);
            }

            return BadRequest(new { message = "Login fail!" });
        }

        private async Task<AuthResultVm> GenerateJWTToken(AppUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add user roles to claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var bookings = await _context.Bookings
                        .Where(b => b.UserId == user.Id)
                        .Select(b => new { b.BookingId, b.BookingDate, b.Status, b.TourId,b.Notes})
                        .ToListAsync();

            if (bookings != null && bookings.Any())
            {
                var bookingsJson = JsonSerializer.Serialize(bookings);
                authClaims.Add(new Claim("bookings", bookingsJson));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(10), // Token expiration time
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            var response = new AuthResultVm()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };

            return response;
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new { message = "Refresh token is required." });
            }

            var storedRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedRefreshToken == null || storedRefreshToken.IsRevoked || storedRefreshToken.DateExpire < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token." });
            }

            // Validate if the JWT token in the refresh token matches any stored token (optional)
            var user = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid user associated with refresh token." });
            }

            // Revoke the old refresh token
            storedRefreshToken.IsRevoked = true;
            _context.RefreshTokens.Update(storedRefreshToken);

            // Generate new tokens
            var newAuthResult = await GenerateJWTToken(user);

            await _context.SaveChangesAsync();

            return Ok(newAuthResult);
        }

        [HttpGet("get-user-info")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var roles = await _userManager.GetRolesAsync(user);

            var bookings = await _context.Bookings
                .Where(b => b.UserId == user.Id)
                .Select(b => new { b.BookingId, b.BookingDate, b.Status, b.TourId, b.Notes })
                .ToListAsync();

            return Ok(new
            {
                user.Id,
                user.Email,
                user.UserName,
                user.FullName,
                Roles = roles,
                Status = true,
                Bookings = bookings
            });
        }

        [HttpPost("update-user-info")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo([FromBody] AppUserVm model)
        {
            // Lấy userId từ token
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            // Tìm user theo userId
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Cập nhật thông tin UserName và FullName
            user.UserName = model.UserName ?? user.UserName; // Nếu không cung cấp UserName, giữ nguyên giá trị cũ
            user.FullName = model.FullName ?? user.FullName; // Nếu không cung cấp FullName, giữ nguyên giá trị cũ

            // Lưu thay đổi
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Failed to update user information.",
                    errors = result.Errors.Select(e => e.Description)
                });
            }

            return Ok(new
            {
                message = "User information updated successfully.",
                user.Id,
                user.Email,
                user.UserName,
                user.FullName
            });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVm model)
        {
            // Lấy người dùng hiện tại từ User.Identity
            var userExit = await _userManager.FindByEmailAsync(model.Email);
            if (userExit == null)
            {
                return Unauthorized("User not found or not logged in.");
            }

            // Thực hiện đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(userExit, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { message = "Password change failed", errors });
            }

            return Ok(new { message = "Password changed successfully!" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var forgotPasswordLink = $"{Request.Scheme}://{Request.Host}/api/Auth/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";
                var forgotPasswordLink = $"http://localhost:4200/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";
                
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", forgotPasswordLink);
                _emailService.SendEmail(message);
                return Ok(new { message = $"Password Changed request is sent on Email  {user.Email}.Please Open your email & click the link!" });
            }
            return Ok(new { message = "Couldn't send link to email, please try again." });

        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new { model });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return Ok(new { message = $"Password has been changed" });
            }
            return Ok(new { message = "Couldn't send link to email, please try again." });

        }
    }

}
