using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class UserExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse(
                user.Id,
                user.UserName,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Attempts?.ToCollectionResponse().ToList(),
                user.QuizzesNavigation.ToCollectionResponse().ToList(),
                user.Quizzes.ToCollectionResponse().ToList(),
                user.Roles.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<UserResponse> ToCollectionResponse(this IEnumerable<User> users)
        {
            return users.Select(ToResponse) ?? new List<UserResponse>();
        }

        public static User ToEntity(this UserRequest userRequest)
        {
            return new User
            {
                UserName = userRequest.UserName,
                PasswordHash = userRequest.Password,  //DODAĆ HASHOWANIE
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
            };
        }
    }
}
