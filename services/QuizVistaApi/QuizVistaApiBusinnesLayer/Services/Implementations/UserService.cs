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
        private readonly IMailService _mailService;

        public UserService(IRepository<User> userRepository,IRepository<Role> roleRepository, IConfiguration configuration, IMailService mailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<ResultWithModel<IEnumerable<UserResponse>>> GetUsers()
        {

            var users = await _userRepository.GetAll().OrderBy(x=>x.Id).ToListAsync();

            return ResultWithModel<IEnumerable<UserResponse>>.Ok(users.ToCollectionResponse().ToList());
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

        public async Task<ResultWithModel<IEnumerable<UserResponse>>> ResetPasswordInit(ResetPasswordInitialRequest request)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var resetToken = GenerateResetToken();

            user.ResetPasswordToken = resetToken;
            await _userRepository.UpdateAsync(user);

            var resetLink = $"[ResetPasswordLink]/?token={resetToken}"; //link do resetu hasła

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Reset Password",
                Body = $"Please click on the link to reset your password: {resetLink}"
            };

            await _mailService.SendEmailAsync(mailRequest);

            return ResultWithModel<IEnumerable<UserResponse>>.Ok(new List<UserResponse> { user.ToResponse() });
        }

        public async Task<Result> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            resetPasswordRequest.Password = HashPassword(resetPasswordRequest.Password);
            resetPasswordRequest.ConfirmPassword = HashPassword(resetPasswordRequest.ConfirmPassword);

            if (string.Compare(resetPasswordRequest.Password, resetPasswordRequest.ConfirmPassword) != 0)
            {
                throw new ArgumentException("The new password and confirm password does not match.");
            }

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.ResetPasswordToken == resetPasswordRequest.Token);

            if (user == null)
            {
                throw new ArgumentException("Invalid or expired reset token.");
            }

            user.PasswordHash = resetPasswordRequest.Password;
            await _userRepository.UpdateAsync(user);

            return Result.Ok();
        }

        public async Task<Result> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            changePasswordRequest.CurrentPassword = HashPassword(changePasswordRequest.CurrentPassword);
            changePasswordRequest.NewPassword = HashPassword(changePasswordRequest.NewPassword);
            changePasswordRequest.ConfirmNewPassword = HashPassword(changePasswordRequest.ConfirmNewPassword);

            if (string.Compare(changePasswordRequest.NewPassword, changePasswordRequest.ConfirmNewPassword) != 0)
            {
                throw new ArgumentException("The new password and confirm password does not match.");
            }

            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName.ToLower() == changePasswordRequest.UserName.ToLower());

            if (user is null)
                throw new ArgumentNullException($"Error");

            if (user.UserName != changePasswordRequest.ValidateUserName) throw new ArgumentException("Forbidden.");

            if (user.PasswordHash != changePasswordRequest.CurrentPassword)
            {
                throw new ArgumentException("Invalid password");
            }

            user.PasswordHash = changePasswordRequest.NewPassword;
            await _userRepository.UpdateAsync(user);
            return Result.Ok();
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


            if (user.Roles != null)
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
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private string GenerateResetToken()
        {
            using var rng = new RNGCryptoServiceProvider();
            var tokenData = new byte[32];
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }
}
