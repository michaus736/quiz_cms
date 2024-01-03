using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Extensions.Mappings;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Requests.AttemptRequests;
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
    public class AttemptService : IAttemptService
    {
        private readonly IRepository<Attempt> _attemptRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Answer> _answerRepository;

        public AttemptService(IRepository<Attempt> attemptRepository, IRepository<User> userRepository, IRepository<Answer> answerRepository)
        {
            _attemptRepository = attemptRepository;
            _userRepository = userRepository;
            _answerRepository = answerRepository;
        }

        public async Task<ResultWithModel<AttemptResponse>> GetAttempt(int id)
        {
            var attempt = await _attemptRepository.GetAsync(id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<AttemptResponse>.Ok(attempt.ToResponse());

        }

        public async Task<ResultWithModel<IEnumerable<AttemptResponse>>> GetAttemptsOfUser(int userId)
        {
            var attempts = await _attemptRepository.GetAll()
                .Where(x=>x.UserId == userId)
                .ToListAsync();

            if (attempts is null)
                throw new ArgumentException($"attemps of user #{userId} not found");

            return ResultWithModel<IEnumerable<AttemptResponse>>.Ok(attempts.ToCollectionResponse().ToList());
        }

        public async Task<ResultWithModel<AttemptResponse>> GetAttemptWithAnswers(int id)
        {
            var attempt = await _attemptRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<AttemptResponse>.Ok(attempt.ToResponse());
        }

        public ResultWithModel<object> GetUserResults(string userName)
        {

            object ob = new object();

            return ResultWithModel<object>.Ok(ob);
        }

        public async Task<Result> SaveAttempt(SaveAttemptRequest attempt, string userName)
        {
            User? user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null) throw new ArgumentNullException($"user {userName} not found");

            var answers = await _answerRepository.GetAll()
                .Where(x => attempt.AnswerIds.Contains(x.Id))
                .ToListAsync();

            if (answers is null) throw new ArgumentNullException($"answers not found");


            Attempt entity = new Attempt
            {
                CreateDate = DateTime.Now,
                EditionDate = DateTime.Now,
                Answers = answers,
                User = user,
                UserId = user.Id
            };


            await _attemptRepository.InsertAsync(entity);


            
            return Result.Ok();
        }

        public async Task<Result> UpdateAttempt(AttemptRequest attempt)
        {
            await _attemptRepository.UpdateAsync(attempt.ToEntity());
            
            return Result.Ok();
        }

        public async Task<Result> DeleteAttempt(int id)
        {
            await _attemptRepository.DeleteAsync(id); 
            
            return Result.Ok();
        }

        
    }
}
