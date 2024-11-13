using Azure;
using BookingTour.Business.Service.IService;
using BookingTour.Data.Data;
using BookingTour.Model;
using BookingTour.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                return BadRequest($"User {registerVm.Email} already exists!");
            }

            var user = new AppUser
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Tạo người dùng nhưng không cho phép đăng nhập cho đến khi xác thực email
            var result = await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "User could not be created" });
            }

            // Tạo token xác thực email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token, email = registerVm.Email }, Request.Scheme);

            // Gửi email xác thực
            var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEmail(message);

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
                // Sau khi xác thực, có thể gán vai trò cho người dùng nếu cần
                await _userManager.AddToRoleAsync(user, "User");
                return Ok(new { message = "Email verified successfully! Your account is now active." });
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

            if (user != null && await _userManager.CheckPasswordAsync(user, loginVm.Password) && user.EmailConfirmed)
            {
                var tokenValue = await GenerateJWTToken(user);
                return Ok(tokenValue);
            }

            return BadRequest(new { message = "Email or password is incorrect!" });
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

        private async Task<AuthResultVm> GenerateJWTToken(AppUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add user roles to claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Thêm BookingId vào claims từ danh sách Bookings của user
            var bookingIds = user.Bookings.Select(b => b.BookingId.ToString()); // Giả sử Booking có thuộc tính Id là ID duy nhất
            foreach (var bookingId in bookingIds)
            {
                authClaims.Add(new Claim("BookingId", bookingId));
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
