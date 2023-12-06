using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System.Security.Claims;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizez()
        {
            return await _quizService.GetQuizesAsync();
        }

        [HttpPost("create"), Authorize(Roles = "User")]
        public async Task<Result> CreateQuiz([FromBody] QuizRequest quizRequest)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;

            return await _quizService.CreateQuizAsync(userId, quizRequest);
        }

        [HttpPut("edit")]
        public async Task<Result> EditQuiz([FromBody] QuizRequest quizRequest)
        {
            return await _quizService.UpdateQuizAsync(quizRequest);
        }

        [HttpDelete("delete")]
        public async Task<Result> DeleteQuiz([FromBody] QuizRequest quizRequest)
        {
            return await _quizService.DeleteQuizAsync(quizRequest.Id);
        }






    }
}
