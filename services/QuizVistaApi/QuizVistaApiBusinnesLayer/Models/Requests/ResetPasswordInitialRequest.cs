using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class ResetPasswordInitialRequest
    {
        public string Email { get; set; }

        public ResetPasswordInitialRequest() { }

        public ResetPasswordInitialRequest(string email)
        {
            Email = email;
        }
    }
}
