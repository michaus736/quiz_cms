using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models.Responses.QuizResponses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class QuizService : IQuizService
    {
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Attempt> _attemptRepository;

        public QuizService(IRepository<Quiz> quizRepository,
            IRepository<User> userRepository,
            IRepository<Tag> tagRepository,
            IRepository<Attempt> attemptRepository)
        {
            _quizRepository = quizRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _attemptRepository = attemptRepository;
        }

        public async Task<Result> CreateQuizAsync(string userId, QuizRequest quizToCreate)
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

            entity.Tags = new List<Tag>();

            var existingTags = await _tagRepository.GetAll()
                .Where(tag => quizToCreate.TagIds.Contains(tag.Id))
                .ToListAsync();

            foreach (var tag in existingTags)
            {
                entity.Tags.Add(tag);
            }



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

        public async Task<Result> UpdateQuizAsync(string userId, QuizRequest quizToUpdate)
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

        public async Task<ResultWithModel<IEnumerable<QuizListForUserResponse>>> GetQuizListForUser(string userName)
        {
            User? loggedUser = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserName == userName);

            if (loggedUser is null) throw new Exception($"user {userName} cannot find");

            List<Quiz> quizes = await _quizRepository.GetAll()
                .Include(x=>x.Users)
                .Include(x=>x.Category)
                .Include(x=>x.Tags)
                .ToListAsync();

            //add public quizes
            IEnumerable<Quiz> quizesAssignedToUser = quizes
                .Where(x => (x.PublicAccess == true) || x.Users.Any(y => y.Id == loggedUser.Id))
                .Where(x => x.IsActive)
                .ToList();

            IList<QuizListForUserResponse> quizesResponse = quizesAssignedToUser.Select(x => new QuizListForUserResponse
            {
                Name = x.Name,
                Description = x.Description ?? "",
                AuthorName = $"{x.Author.FirstName} {x.Author.LastName}",
                CategoryName = x.Category.Name,
                Tags = x.Tags.Select(y=>new TagResponse
                {
                    Id = y.Id,
                    Name = y.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList()
            }).ToList();

            return ResultWithModel<IEnumerable<QuizListForUserResponse>>.Ok(quizesResponse);
        }

        public async Task<ResultWithModel<QuizDetailsForUser>> GetQuizDetailsForUser(string quizName, string userName)
        {
            //getting quiz props
            Quiz? quiz = await _quizRepository.GetAll()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Name == quizName);

            if (quiz is null) throw new ArgumentException($"quiz {quizName} does not exist");

            //getting user attempts count
            User? user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null) throw new ArgumentException($"user does not exist");

            IList<Attempt> userAttempts = await _attemptRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Answers)
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            QuizDetailsForUser quizDetails = new QuizDetailsForUser
            {
                Name = quiz.Name,
                Description = quiz.Description,
                AuthorName = $"{quiz.Author.FirstName} {quiz.Author.LastName}",
                CategoryName = quiz.Category.Name,
                AttemptsLimit = quiz.AttemptCount,
                Tags = quiz.Tags.Select(x => new TagResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quizzes = new List<QuizResponse>()
                }).ToList(),
                UserAttempts = userAttempts.Count
            };



            return ResultWithModel<QuizDetailsForUser>.Ok(quizDetails);

        }
    }
}
