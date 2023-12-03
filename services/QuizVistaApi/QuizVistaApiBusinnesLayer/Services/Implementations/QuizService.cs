using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
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

        public async Task<Result> CreateQuizAsync(QuizRequest quizToCreate)
        {
            var entity = quizToCreate.ToEntity();

            entity.CreationDate = DateTime.Now;
            entity.EditionDate = DateTime.Now;


            await _quizRepository.InsertAsync(entity);

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

            return ResultWithModel<QuizResponse>.Ok(quiz.ToResponse());
        }

        public async Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizesAsync()
        {
            var quizes = await _quizRepository
                .GetAll()
                //.Include(x=>x.Author)
                .OrderBy(x=>x.Id)
                .ToListAsync();

            if(quizes is null)
                throw new ArgumentNullException(nameof(quizes));

            var quizesResponses = quizes.ToCollectionResponse().ToList();

            return ResultWithModel<IEnumerable<QuizResponse>>.Ok(quizesResponses);

        }

        public async Task<ResultWithModel<QuizResponse>> GetQuizWithQuestionsAsync(int id)
        {
            var quiz = await _quizRepository.GetAll()
                .Include(x => x.Questions)
                .FirstOrDefaultAsync( x=> x.Id == id);

            if (quiz is null)
                throw new ArgumentNullException($"quiz #{id} not found");

            return ResultWithModel<QuizResponse>.Ok(quiz.ToResponse());
        }

        public async Task<Result> UpdateQuizAsync(QuizRequest quizToUpdate)
        {
            await _quizRepository.UpdateAsync(quizToUpdate.ToEntity());

            return Result.Ok();
        }
    }
}
