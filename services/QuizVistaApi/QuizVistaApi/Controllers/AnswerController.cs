using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using System.Collections.Generic;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswers()
        {
            return await _answerService.GetAnswers();
        }

        [HttpGet("answer")]
        public async Task<ResultWithModel<AnswerResponse>> GetAnswer([FromBody] AnswerRequest answerRequest)
        {
            return await _answerService.GetAnswer(answerRequest);
        }

        [HttpGet("answerForQuestion")]
        public async Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswersForQuestion([FromBody] QuestionRequest questionRequest)
        {
            return await _answerService.GetAnswersForQuestion(questionRequest.Id);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerRequest answerRequest)
        {
            var result = await _answerService.CreateAnswerAsync(answerRequest);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<Result> EditAnswer([FromBody] AnswerRequest answerRequest)
        {
            return await _answerService.UpdateAnswerAsync(answerRequest);
        }

        [HttpDelete("delete")]
        public async Task<Result> DeleteAnswer([FromBody] AnswerRequest answerRequest)
        {
            return await _answerService.DeleteAnswerAsync(answerRequest.Id);
        }

    }
}
