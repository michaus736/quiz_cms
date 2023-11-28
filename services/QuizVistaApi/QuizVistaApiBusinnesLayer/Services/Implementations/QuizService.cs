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
    public class QuizService : IQuizService
    {
        private readonly IRepository<Quiz> _quizRepository;

        public QuizService(IRepository<Quiz> quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task CreateQuizAsync(Quiz quizToCreate)
        {
            await _quizRepository.InsertAsync(quizToCreate);
        }

        public async Task DeleteQuizAsync(int idToDelete)
        {
            await _quizRepository.DeleteAsync(idToDelete);
        }

        public async Task<ResultWithModel<Quiz>> GetQuizAsync(int id)
        {
            var quiz = await _quizRepository.GetAsync(id);

            if(quiz is null)
                throw new ArgumentNullException($"quiz #{id} not found");

            return ResultWithModel<Quiz>.Ok(quiz);
        }

        public async Task<ResultWithModel<IEnumerable<Quiz>>> GetQuizesAsync()
        {
            var quizes = await _quizRepository
                .GetAll()
                .OrderBy(x=>x.Id)
                .ToListAsync();

            if(quizes is null)
                throw new ArgumentNullException(nameof(quizes));

            return ResultWithModel<IEnumerable<Quiz>>.Ok(quizes);

        }

        public Task<ResultWithModel<Quiz>> GetQuizWithQuestionsAsync(int id)
        {
            var quiz = _quizRepository.GetAll()
                .Include(x => x.Questions)
                .FirstOrDefault( x=> x.Id == id);

            if (quiz is null)
                throw new ArgumentNullException($"quiz #{id} not found");

            return Task.FromResult(ResultWithModel<Quiz>.Ok(quiz));
        }

        public async Task UpdateQuizAsync(Quiz quizToUpdate)
        {
            await _quizRepository.UpdateAsync(quizToUpdate);
        }
    }
}
