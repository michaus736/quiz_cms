using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;

namespace QuizVistaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : ControllerBase
    {

        private readonly IAttemptService _attemptService;

        public AttemptController(IAttemptService attemptService)
        {
            _attemptService = attemptService;
        }

        [HttpGet]
        public async Task<ResultWithModel<AttemptResponse>> GetAttempts([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.GetAttempt(attemptRequest.UserId);
        }

        [HttpGet("forUser")]
        public async Task<ResultWithModel<IEnumerable<AttemptResponse>>> GetAttemptsForUser([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.GetAttemptsOfUser(attemptRequest.UserId);
        }

        [HttpPost("create")]
        public async Task<Result> CreateAttempt([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.SaveAttempt(attemptRequest);
        }

        [HttpPut("edit")]
        public async Task<Result> EditAttempt([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.SaveAttempt(attemptRequest);
        }

        [HttpDelete("delete")]
        public async Task<Result> DeleteAttempt([FromBody] AttemptRequest attemptRequest)
        {
            return await _attemptService.DeleteAttempt(attemptRequest.Id);
        }







    }
}
