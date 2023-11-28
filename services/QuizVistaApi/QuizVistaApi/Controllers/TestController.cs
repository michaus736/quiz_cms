using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;

namespace QuizVistaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Result Get()
        {
            return Result.Ok();
        }
    }
}