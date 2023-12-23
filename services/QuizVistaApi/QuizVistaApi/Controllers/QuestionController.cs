using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiBusinnesLayer.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestions()
        {
            return await _questionService.GetQuestions();
        }
        
        [HttpPost("create")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> CreateQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _questionService.CreateQuestionAsync(questionRequest);
        }
        
        [HttpPut("edit")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> EditQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _questionService.UpdateQuestionAsync(questionRequest);
        }
      
        [HttpDelete("delete")]
        [Authorize(Roles = "Moderator")]
        public async Task<Result> DeleteQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _questionService.DeleteQuestionAsync(questionRequest.Id);
        }
    }
}
