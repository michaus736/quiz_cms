using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class ChangePasswordRequest
    {
        public string ValidateUserName { get; set; }
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword {  get; set; }
        public string ConfirmNewPassword {  get; set; }

        public ChangePasswordRequest() { }

        public ChangePasswordRequest(string userName, string currentPassword, string newPassword, string confirmNewPassword)
        {
            UserName = userName;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
    }

}
