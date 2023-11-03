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

        public async Task<ModelWithResult<Answer>> GetAnswer(int answerId)
        {
            var answer = await _answerRepository.GetAsync(answerId);

            if(answer is null)
                throw new ArgumentNullException(nameof(answer));

            return new ModelWithResult<Answer>(answer);
        }

        public Task<ModelWithResult<IEnumerable<Answer>>> GetAnswers()
        {
            return Task.FromResult(new ModelWithResult<IEnumerable<Answer>>(_answerRepository.GetAll().ToArray()));
        }

        public async Task<ModelWithResult<IEnumerable<Answer>>> GetAnswersForQuestion(int questionId)
        {
            var answers =  _answerRepository
                .GetAll()
                .Where(x=>x.QuestionQuestionId == questionId)
                .OrderBy(x=>x.AnswerId)
                .ToList();

            if(answers is null)
                throw new ArgumentNullException(nameof(answers));

            return await Task.FromResult(new ModelWithResult<IEnumerable<Answer>>(answers));
            
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            await _answerRepository.UpdateAsync(answer);
        }
    }
}
