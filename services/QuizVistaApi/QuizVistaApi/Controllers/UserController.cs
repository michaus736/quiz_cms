using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
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


    }
}
