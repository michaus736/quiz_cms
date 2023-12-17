using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System.Security.Claims;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("test")]
        public Result Test()
        {
            return Result.Ok();
        }

        [HttpPost("register")]
        public async Task<Result> Register([FromBody] UserRequest userRequest)
        {
            return await _userService.RegisterUser(userRequest);
        }

        [HttpPost("login")]
        public async Task<ResultWithModel<LoginResponse>> Login([FromBody] UserRequest userRequest)
        {
            return await _userService.LoginUser(userRequest);
        }

        [HttpGet("showusers"), Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<UserResponse>>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpPut("edit")]
        public async Task<Result> Edit([FromBody] UserRequest userRequest)
        {
            return await _userService.UpdateUser(userRequest);
        }

        [HttpPost("changepassword"), Authorize(Roles = "User")]
        public async Task<Result> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            changePasswordRequest.ValidateUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _userService.ChangePassword(changePasswordRequest);
        }

        [HttpPost("reset-password-init")]
        public async Task<ResultWithModel<IEnumerable<UserResponse>>> ResetPasswordInit([FromBody] ResetPasswordInitialRequest resetPasswordInitialRequest)
        {
            return await _userService.ResetPasswordInit(resetPasswordInitialRequest);
        }

        [HttpPost("reset-password")]
        public async Task<Result> ResetPassword([FromBody] ResetPasswordRequest resetPassRequest)
        {
            return await _userService.ResetPassword(resetPassRequest);
        }
    }
}
