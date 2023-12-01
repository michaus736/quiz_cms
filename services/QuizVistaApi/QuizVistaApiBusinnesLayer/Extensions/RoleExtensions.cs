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
    public static class RoleExtensions
    {

        public static RoleResponse ToResponse(this Role role)
        {
            return new RoleResponse(
                role.Id,
                role.Name,
                role.Users.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<RoleResponse> ToCollectionResponse(this IEnumerable<Role> roles)
        {
            return roles.Select(ToResponse) ?? new List<RoleResponse>();
        }

        public static Role ToEntity(this RoleRequest roleRequest)
        {
            return new Role
            {
                Name = roleRequest.Name,
            };
        }

    }
}
