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
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task CreateQuestionAsync(Question question)
        {
            await _questionRepository.InsertAsync(question);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            await _questionRepository.DeleteAsync(questionId);
        }

        public async Task<ModelWithResult<Question>> GetQuestion(int questionId)
        {
            var question = await _questionRepository.GetAsync(questionId);

            if(question is null) 
                throw new ArgumentNullException(nameof(question));

            return new ModelWithResult<Question>(question);
        }

        public async Task<ModelWithResult<IEnumerable<Question>>> GetQuestions()
        {
            var questions = await _questionRepository.GetAll()
                .OrderBy(x => x.QuestionId)
                .ToListAsync();

            return new ModelWithResult<IEnumerable<Question>>(questions);
        }

        public async Task<ModelWithResult<IEnumerable<Question>>> GetQuestionsForQuiz(int quizId)
        {
            var questions = await _questionRepository.GetAll()
                .Where(x=>x.QuizQuizId == quizId)
                .OrderBy(x=> x.QuestionId)
                .ToListAsync();

            if(questions is null)
                throw new ArgumentNullException(nameof(questions));

            return new ModelWithResult<IEnumerable<Question>>(questions);

        }

        public Task<ModelWithResult<Question>> GetQuestionWithAnswers(int questionId)
        {
            var questionExtended = _questionRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefault(x => x.QuestionId == questionId);

            if(questionExtended is null)
                throw new ArgumentNullException(nameof(questionExtended));

            return Task.FromResult(new ModelWithResult<Question>(questionExtended));

        }

        public async Task UpdateQuestionAsync(Question question)
        {
            await _questionRepository.UpdateAsync(question);
        }
    }
}
