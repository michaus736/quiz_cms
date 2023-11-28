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

        public async Task<ResultWithModel<Question>> GetQuestion(int questionId)
        {
            var question = await _questionRepository.GetAsync(questionId);

            if(question is null) 
                throw new ArgumentNullException($"question #{questionId} not found");

            return ResultWithModel<Question>.Ok(question);
        }

        public async Task<ResultWithModel<IEnumerable<Question>>> GetQuestions()
        {
            var questions = await _questionRepository.GetAll()
                .OrderBy(x => x.Id)
                .ToListAsync();

            return ResultWithModel<IEnumerable<Question>>.Ok(questions);
        }

        public async Task<ResultWithModel<IEnumerable<Question>>> GetQuestionsForQuiz(int quizId)
        {
            var questions = await _questionRepository.GetAll()
                .Where(x=>x.QuizId == quizId)
                .OrderBy(x=> x.Id)
                .ToListAsync();

            if(questions is null)
                throw new ArgumentNullException($"questions for quiz #{quizId} not found");

            return ResultWithModel<IEnumerable<Question>>.Ok(questions);

        }

        public Task<ResultWithModel<Question>> GetQuestionWithAnswers(int questionId)
        {
            var questionExtended = _questionRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefault(x => x.Id == questionId);

            if(questionExtended is null)
                throw new ArgumentNullException($"question #{questionId} not found");

            return Task.FromResult(ResultWithModel<Question>.Ok(questionExtended));

        }

        public async Task UpdateQuestionAsync(Question question)
        {
            await _questionRepository.UpdateAsync(question);
        }
    }
}
