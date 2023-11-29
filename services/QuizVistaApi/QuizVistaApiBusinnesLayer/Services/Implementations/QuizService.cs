using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Responses;
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

        public async Task<Result> CreateQuizAsync(QuizResponse quizToCreate)
        {
            await _quizRepository.InsertAsync(quizToCreate);

            return Result.Ok();
        }

        public async Task<Result> DeleteQuizAsync(int idToDelete)
        {
            await _quizRepository.DeleteAsync(idToDelete);

            return Result.Ok();
        }

        public async Task<ResultWithModel<QuizResponse>> GetQuizAsync(int id)
        {
            var quiz = await _quizRepository.GetAsync(id);

            if(quiz is null)
                throw new ArgumentNullException($"quiz #{id} not found");

            return ResultWithModel<QuizResponse>.Ok(quiz.Convert());
        }

        public async Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizesAsync()
        {
            var quizes = await _quizRepository
                .GetAll()
                .OrderBy(x=>x.Id)
                .ToListAsync();

            if(quizes is null)
                throw new ArgumentNullException(nameof(quizes));

            return ResultWithModel<IEnumerable<QuizResponse>>.Ok(quizes.ConvertCollection().ToList());

        }

        public async Task<ResultWithModel<QuizResponse>> GetQuizWithQuestionsAsync(int id)
        {
            var quiz = await _quizRepository.GetAll()
                .Include(x => x.Questions)
                .FirstOrDefaultAsync( x=> x.Id == id);

            if (quiz is null)
                throw new ArgumentNullException($"quiz #{id} not found");

            return ResultWithModel<QuizResponse>.Ok(quiz.Convert());
        }

        public async Task<Result> UpdateQuizAsync(QuizResponse quizToUpdate)
        {
            await _quizRepository.UpdateAsync(quizToUpdate);

            return Result.Ok();
        }
    }
}
