using Microsoft.AspNetCore.Identity;
using QuizVistaApi.Model;

namespace QuizVistaApi.Data
{
    public class AppDbInitializer
    {
       public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            CreateRoles(context);
        }

        private static void CreateRoles(ApplicationDbContext context)
        {
            if (!context.Role.Any())
            {
                var roles = new List<Role>
                {
                    new Role {Name = "User"},
                    new Role {Name="Admin"}
                };
                context.Role.AddRange(roles);
                context.SaveChanges();
            }
        }
    }
}
