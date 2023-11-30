using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class UserExtensions
    {
        public static UserResponse Convert(this User user)
        {
            return new UserResponse(
                user.Id,
                user.UserName,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Attempts.ConvertCollection().ToList(),
                user.QuizzesNavigation.ConvertCollection().ToList(),
                user.Quizzes.ConvertCollection().ToList(),
                user.Roles.ConvertCollection().ToList()
            );
        }

        public static IEnumerable<UserResponse> ConvertCollection(this IEnumerable<User> users)
        {
            return users.Select(Convert) ?? new List<UserResponse>();
        }

        public static User ToEntity(this UserRequest userRequest)
        {
            return new User
            {
                UserName= userRequest.UserName,
                PasswordHash = userRequest.Password,  //DODAĆ HASHOWANIE
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
            };
        }
    }
}
