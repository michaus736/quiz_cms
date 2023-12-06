using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;

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
        public async Task<Result> Login([FromBody] UserRequest userRequest)
        {
            return await _userService.LoginUser(userRequest);
        }

        [HttpGet("showusers"), Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<UserResponse>>> GetUsers()
        {
            return await _userService.GetUsers();
        }



    }
}
