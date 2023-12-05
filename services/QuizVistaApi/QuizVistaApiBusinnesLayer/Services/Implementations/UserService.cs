using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IConfiguration _configuration;

        public UserService(IRepository<User> userRepository,IRepository<Role> roleRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        public async Task<Result> RegisterUser(UserRequest request)
        {
            var users = _userRepository.GetAll();
            bool emailCheck = users.Any(x => x.Email.ToLower() == request.Email.ToLower());
            bool userNameCheck = users.Any(x=>x.UserName.ToLower() == request.UserName.ToLower());
            if (emailCheck)
                throw new ArgumentException("actual email is already registered");
            if (userNameCheck)
                throw new ArgumentException("user with this username is already registered");

            request.Password = HashPassword(request.Password);

            var newUser = request.ToEntity();
            await _userRepository.InsertAsync(newUser);

            var userRole = new Role { Id = 3, Name = "User" };

            newUser.Roles.Add(userRole);
            await _userRepository.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> LoginUser(UserRequest request)
        {

            request.Password = HashPassword(request.Password);


            var user = await _userRepository.GetAll().Include(x=>x.Roles).Where(x=>x.UserName.ToLower() == request.UserName.ToLower()).FirstOrDefaultAsync();

            if (user is null)
                throw new ArgumentNullException($"User with this username is not registered");


            if (user.PasswordHash != request.Password)
                throw new ArgumentException("Invalid password");

           string token = CreateToken(user);

            //return Result.Ok(new { token });
            return Result.Ok(token);
        }

        static string HashPassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
     {
         new Claim(ClaimTypes.Name,user.UserName),
     };


            if (user.Roles != null) // Sprawdź, czy user.UserRoles nie jest null
            {
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
