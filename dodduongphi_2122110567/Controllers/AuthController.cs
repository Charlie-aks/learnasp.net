using dodduongphi_2122110567.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using dodduongphi_2122110567.Data;

namespace dodduongphi_2122110567.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context; // ✅ THÊM
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public AuthController(IWebHostEnvironment environment, IConfiguration configuration, AppDbContext context) // ✅ THÊM context
        {
            _environment = environment;
            _configuration = configuration;
            _context = context;
        }

        // ✅ Đăng ký - POST (với avatar)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterDto userDto)
        {
            // Kiểm tra username đã tồn tại
            if (_context.Users.Any(u => u.Username == userDto.Username))
                return BadRequest("Username already exists");

            // Ràng buộc validation
            if (string.IsNullOrWhiteSpace(userDto.Username) || userDto.Username.Length < 6)
                return BadRequest("Username must be at least 6 characters long");

            if (string.IsNullOrWhiteSpace(userDto.Password) || userDto.Password.Length < 8)
                return BadRequest("Password must be at least 8 characters long");

            if (!string.IsNullOrWhiteSpace(userDto.Email) && !IsValidEmail(userDto.Email))
                return BadRequest("Invalid email format");

            if (!string.IsNullOrWhiteSpace(userDto.Phone) && !IsValidPhoneNumber(userDto.Phone))
                return BadRequest("Phone number must be at least 10 digits");

            // Xử lý upload avatar
            string avatarPath = string.Empty;
            if (userDto.AvatarFile != null && userDto.AvatarFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "avatars");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Kiểm tra định dạng file
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(userDto.AvatarFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Only image files (jpg, jpeg, png, gif) are allowed");
                }

                // Giới hạn kích thước file (5MB)
                if (userDto.AvatarFile.Length > 5 * 1024 * 1024)
                {
                    return BadRequest("File size must be less than 5MB");
                }

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await userDto.AvatarFile.CopyToAsync(fileStream);
                }

                avatarPath = $"/avatars/{uniqueFileName}";
            }

            // Tạo user mới
            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password, // Trong thực tế nên hash password
                Email = userDto.Email,
                FullName = userDto.FullName,
                Address = userDto.Address,
                Phone = userDto.Phone,
                Role = string.IsNullOrWhiteSpace(userDto.Role) ? "Customer" : userDto.Role,
                Avatar = avatarPath
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "User registered successfully",
                User = new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.FullName,
                    user.Role,
                    user.Avatar
                }
            });
        }

        // ✅ Đăng nhập - POST
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = GenerateJwtToken(user);
            return Ok(new
            {
                Token = token,
                User = new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.FullName,
                    user.Role,
                    user.Avatar
                }
            });
        }

        // ✅ Lấy thông tin user hiện tại - GET
        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound("User not found");

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.FullName,
                user.Role,
                user.Avatar
            });
        }

        // ✅ Endpoint yêu cầu xác thực - GET
        [Authorize]
        [HttpGet("secure")]
        public IActionResult SecureEndpoint()
        {
            return Ok(new
            {
                Message = "Bạn đã xác thực thành công!",
                UserClaims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

        // ✅ Endpoint yêu cầu role Admin - GET
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Chỉ Admin mới có thể truy cập endpoint này!");
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? "4F1B@L2!jY8mC^aTzW3#Qv6KdXpRnUeZ")); // Fallback key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Avatar", user.Avatar ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return phone.Length >= 10 && phone.All(char.IsDigit);
        }
    }

    public class UserRegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public IFormFile? AvatarFile { get; set; }
    }

    public class UserLoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}