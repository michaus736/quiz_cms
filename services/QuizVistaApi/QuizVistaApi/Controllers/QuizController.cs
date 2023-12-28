using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiBusinnesLayer.Services.Implementations;
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
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizez()
        {
            return await _quizService.GetQuizesAsync();
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizesForUser()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizListForUser(userName);
        }

        [HttpGet("details")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<QuizDetailsForUser>> GetQuizDetails(string quizName)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.GetQuizDetailsForUser(quizName, userName);
        }

        [HttpGet("quiz-run")]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<QuizRun>> GetUserQuizRun(string quizName)
        {
            return await _quizService.GetQuizWithQuestionsAsync(quizName);
        }


        [HttpPost("create"), Authorize(Roles = "Moderator")]
        public async Task<Result> CreateQuiz([FromBody] QuizRequest quizRequest)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            //quizRequest.userID = User.FindFirst(ClaimTypes.Name)?.Value;


            return await _quizService.CreateQuizAsync(userId, quizRequest);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> EditQuiz([FromBody] QuizRequest quizRequest)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.UpdateQuizAsync(userId,quizRequest);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> DeleteQuiz([FromBody] QuizRequest quizRequest)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return await _quizService.DeleteQuizAsync(userId,quizRequest.Id);
        }

        [HttpPost("assignuser")]
        [Authorize(Roles="Moderator")]
        public async Task<Result> AssignUser([FromBody] AssignUserRequest assignUserRequest)
        {
            return await _quizService.AssignUser(assignUserRequest);
        }

        [HttpPost("unassignuser")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> UnAssignUser([FromBody] AssignUserRequest assignUserRequest)
        {
            return await _quizService.UnAssignUser(assignUserRequest);
        }



    }
}
