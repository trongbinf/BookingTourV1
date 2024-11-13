using BookingTour.Data.Data;
using BookingTour.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public AuthController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            , BookingTourDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVm registerVm)
        {
            //check user exists

            var userExists = await _userManager.FindByEmailAsync(registerVm.Email);

            if (userExists != null)
            {
                return BadRequest($"User {registerVm.Email} already exists!");
            }

            var user = new AppUser
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                SecurityStamp = new Guid().ToString()
            };

            if (await _roleManager.RoleExistsAsync("User"))
            {
                var result = await _userManager.CreateAsync(user, registerVm.Password);
                if (!result.Succeeded)
                {
                    return BadRequest("User could not be created");
                }
                await _userManager.AddToRoleAsync(user, "User");
                // Return JSON response with success message

                return Created(nameof(Register), new { message = $"User {registerVm.Email} created!" });
            }
            else
            {
                return BadRequest(new { message = "User could not be created" });
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

            if (user != null && await _userManager.CheckPasswordAsync(user, loginVm.Password))
            {
                var tokenValue = await GenerateJWTToken(user);
                return Ok(tokenValue);
            }

            return BadRequest(new { message = "Email or password is incorrect!" });
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
    }

}
