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

        public async Task<ModelWithResult<Quiz>> GetQuizAsync(int id)
        {
            var quiz = await _quizRepository.GetAsync(id);

            if(quiz is null)
                throw new ArgumentNullException(nameof(quiz));

            return new ModelWithResult<Quiz>(quiz);
        }

        public async Task<ModelWithResult<IEnumerable<Quiz>>> GetQuizesAsync()
        {
            var quizes = await _quizRepository
                .GetAll()
                .OrderBy(x=>x.QuizId)
                .ToListAsync();

            if(quizes is null)
                throw new ArgumentNullException(nameof(quizes));

            return new ModelWithResult<IEnumerable<Quiz>>(quizes);

        }

        public Task<ModelWithResult<Quiz>> GetQuizWithQuestionsAsync(int id)
        {
            var quiz = _quizRepository.GetAll()
                .Include(x => x.Questions)
                .FirstOrDefault( x=> x.QuizId == id);

            if (quiz is null)
                throw new ArgumentNullException(nameof(quiz));

            return Task.FromResult(new ModelWithResult<Quiz>(quiz);
        }

        public async Task UpdateQuizAsync(Quiz quizToUpdate)
        {
            await _quizRepository.UpdateAsync(quizToUpdate);
        }
    }
}
