using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> RegisterUser(UserRequest request)
        {
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

            var users = _userRepository.GetAll();
            bool emailCheck = users.Any(x => x.Email.ToLower() == request.Email.ToLower());
            bool userNameCheck = users.Any(x=>x.UserName.ToLower() == request.UserName.ToLower());
            if (emailCheck)
                throw new ArgumentException("actual email is already registered");
            if (userNameCheck)
                throw new ArgumentException("user with this username is already registered");

            request.Password = HashPassword(request.Password);

            await _userRepository.InsertAsync(request.ToEntity());

            return Result.Ok();
        }
    }
}
