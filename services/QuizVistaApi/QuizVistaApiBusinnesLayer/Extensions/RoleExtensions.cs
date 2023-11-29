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

        public static RoleResponse Convert(this Role role)
        {
            return new RoleResponse(
                role.Id,
                role.Name,
                role.Users.ConvertCollection().ToList()
            );
        }

        public static IEnumerable<RoleResponse> ConvertCollection(this IEnumerable<Role> roles)
        {
            return roles.Select(Convert) ?? new List<RoleResponse>();
        }

    }
}
