using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;

namespace QuizVistaApi.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerRequest answerRequest)
        {
            var result = await _answerService.CreateAnswerAsync(answerRequest);
            return Ok(result);
        }
    }
}
