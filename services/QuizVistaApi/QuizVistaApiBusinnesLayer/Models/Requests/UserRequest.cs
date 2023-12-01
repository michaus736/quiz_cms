using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class UserRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserRequest() { }

        public UserRequest(string userName, string password, string firstName, string lastName, string email)
        {
            UserName = userName;
            Password = password; // Hasło będzie hashowane w systemie
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
