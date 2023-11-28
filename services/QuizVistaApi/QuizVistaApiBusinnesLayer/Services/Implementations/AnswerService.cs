using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> _answerRepository;

        public AnswerService(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task CreateAnswerAsync(Answer answer)
        {
            await _answerRepository.InsertAsync(answer);
            
        }

        public async Task DeleteAnswerAsync(int answerId)
        {
            await _answerRepository.DeleteAsync(answerId);
            
        }

        public async Task<ResultWithModel<Answer>> GetAnswer(int answerId)
        {
            var answer = await _answerRepository.GetAsync(answerId);

            if(answer is null)
                throw new ArgumentNullException($"answer #{answer} not found");

            return ResultWithModel<Answer>.Ok(answer);
        }

        public Task<ResultWithModel<IEnumerable<Answer>>> GetAnswers()
        {
            return Task.FromResult(ResultWithModel<IEnumerable<Answer>>.Ok(_answerRepository.GetAll().ToArray()));
        }

        public async Task<ResultWithModel<IEnumerable<Answer>>> GetAnswersForQuestion(int questionId)
        {
            var answers =  _answerRepository
                .GetAll()
                .Where(x=>x.QuestionId == questionId)
                .OrderBy(x=>x.Id)
                .ToList();

            if (answers is null)
                throw new ArgumentNullException($"answers for #{questionId} question not found");

            return await Task.FromResult(ResultWithModel<IEnumerable<Answer>>.Ok(answers));
            
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            await _answerRepository.UpdateAsync(answer);
        }
    }
}
