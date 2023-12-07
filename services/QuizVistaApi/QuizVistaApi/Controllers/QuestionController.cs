using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Services.Implementations;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiBusinnesLayer.Models.Requests;

namespace QuizVistaApi.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestions()
        {
            return await _questionService.GetQuestions();
        }

        [HttpPost("create")]
        public async Task<Result> CreateQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _questionService.CreateQuestionAsync(questionRequest);
        }

        [HttpPut("edit")]
        public async Task<Result> EditQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _questionService.UpdateQuestionAsync(questionRequest);
        }

        [HttpDelete("delete")]
        public async Task<Result> DeleteQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _questionService.DeleteQuestionAsync(questionRequest.Id);
        }
    }
}
