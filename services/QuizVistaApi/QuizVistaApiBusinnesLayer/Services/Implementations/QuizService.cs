using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepository<User> _userRepository;

        public QuizService(IRepository<Quiz> quizRepository, IRepository<User> userRepository)
        {
            _quizRepository = quizRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> CreateQuizAsync(string userId,QuizRequest quizToCreate)
        {
            var entity = quizToCreate.ToEntity();

            var user = await _userRepository.GetAll().Where(x=>x.UserName == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result.Failed("User ID is missing.");
            }

            entity.AuthorId = user.Id;

            entity.CreationDate = DateTime.Now;
            entity.EditionDate = DateTime.Now;


            await _quizRepository.InsertAsync(entity);

            return Result.Ok();
        }

        public async Task<Result> DeleteQuizAsync(string userId, int idToDelete)
        {
            var quiz = await _quizRepository.GetAsync(idToDelete);

            var user = await _userRepository.GetAll().Where(x => x.UserName == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            if (user.Id != quiz.AuthorId)
            {
                return Result.Failed("Unauthorized user.");
            }

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

        public async Task<Result> UpdateQuizAsync(string userId,QuizRequest quizToUpdate)
        {
            var existingQuiz = await _quizRepository.GetAll().FirstOrDefaultAsync(q => q.Id == quizToUpdate.Id);

            if (existingQuiz == null)
            {
                return Result.Failed("Quiz not found.");
            }

            var user = await _userRepository.GetAll().Where(x => x.UserName == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            if (existingQuiz.AuthorId != user.Id)
            {
                return Result.Failed("Unauthorized user.");
            }

            var updatedEntity = quizToUpdate.ToEntity();

            existingQuiz.Name = updatedEntity.Name;
            existingQuiz.Description = updatedEntity.Description;
            existingQuiz.CategoryId = updatedEntity.CategoryId;
            existingQuiz.CmsTitleStyle = updatedEntity.CmsTitleStyle;
            existingQuiz.IsActive = updatedEntity.IsActive;
            existingQuiz.PublicAccess= updatedEntity.PublicAccess;
            existingQuiz.Tags = updatedEntity.Tags;
            existingQuiz.EditionDate = DateTime.Now;

            await _quizRepository.UpdateAsync(existingQuiz);

            return Result.Ok();
        }

        public async Task<Result> AssignUser(AssignUserRequest assignUserRequest)
        {
            var quiz = await _quizRepository.GetAll().Include(x=>x.Users).Where(x=>x.Id==assignUserRequest.QuizId).FirstOrDefaultAsync();
            if (quiz == null)
            {
                return Result.Failed("Quiz not found.");
            }


            var user = await _userRepository.GetAll().Where(x => x.UserName.ToLower() == assignUserRequest.UserName.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            //var x = quiz.Users.Where(x=>x.Id==user.Id).FirstOrDefault();

            if (quiz.Users.Any(x=>x.Id==user.Id))
            {
                return Result.Failed("User is already assigned to this quiz.");
            }

            quiz.Users.Add(user);
            await _quizRepository.UpdateAsync(quiz);

            return Result.Ok();
        }

        public async Task<Result> UnAssignUser(AssignUserRequest assignUserRequest)
        {
            var quiz = await _quizRepository.GetAll().Include(x => x.Users).Where(x => x.Id == assignUserRequest.QuizId).FirstOrDefaultAsync();
            if (quiz == null)
            {
                return Result.Failed("Quiz not found.");
            }
            var user = await _userRepository.GetAll().Where(x => x.UserName.ToLower() == assignUserRequest.UserName.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                return Result.Failed("User not found.");
            }

            if (!quiz.Users.Any(x => x.Id == user.Id))
            {
                return Result.Failed("User is not assigned to this quiz.");
            }

            quiz.Users.Remove(user);
            await _quizRepository.UpdateAsync(quiz);

            return Result.Ok();
        }
    }
}
