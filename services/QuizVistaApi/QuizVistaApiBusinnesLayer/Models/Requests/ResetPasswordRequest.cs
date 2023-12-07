using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set;}

        public ResetPasswordRequest() { }

        public ResetPasswordRequest(string token, string password, string confirmPassword)
        {
            Token = token;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
