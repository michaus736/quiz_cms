using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterUser(UserRequest request);
        Task<ResultWithModel<LoginResponse>> LoginUser(UserRequest request);
        Task<Result> ChangePassword(ChangePasswordRequest changePasswordRequest);
        Task<Result> UpdateUser(UserRequest userRequest);
        Task<ResultWithModel<IEnumerable<UserResponse>>> ResetPasswordInit(ResetPasswordInitialRequest resetPasswordInitialRequest);
        Task<Result> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<ResultWithModel<IEnumerable<UserResponse>>> GetUsers();
    }
}
