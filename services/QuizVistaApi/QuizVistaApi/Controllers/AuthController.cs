using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizVistaApi.Data;
using QuizVistaApi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            string passwordHash= BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email=request.Email
            };

        
            
            _context.User.Add(user);
            _context.SaveChanges();

            var userRole = new UserRole
            {
                UserId = user.UserId,
                RoleId = 1
            };

            _context.UserRole.Add(userRole);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDtoLogin request)
        {
            var user = _context.User
    .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
    .FirstOrDefault(u => u.Username == request.Username);



            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Sprawdź, czy hasło jest prawidłowe
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password");
            }

            // Jeśli dane logowania są poprawne, wygeneruj token dostępowy
            string token = CreateToken(user);

            return Ok(new { token });
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
            };


            if (user.UserRoles != null) // Sprawdź, czy user.UserRoles nie jest null
            {
                foreach (var role in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddMinutes(5),
                signingCredentials:creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
