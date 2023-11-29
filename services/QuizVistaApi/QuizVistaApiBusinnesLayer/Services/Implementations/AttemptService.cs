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
    public class AttemptService : IAttemptService
    {
        private readonly IRepository<Attempt> _attemptRepository;

        public AttemptService(IRepository<Attempt> attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }

        public async Task<ResultWithModel<AttemptResponse>> GetAttempt(int id)
        {
            var attempt = await _attemptRepository.GetAsync(id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<AttemptResponse>.Ok(attempt.Convert());

        }

        public async Task<ResultWithModel<IEnumerable<AttemptResponse>>> GetAttemptsOfUser(int userId)
        {
            var attempts = await _attemptRepository.GetAll()
                .Where(x=>x.UserId == userId)
                .ToListAsync();

            if (attempts is null)
                throw new ArgumentException($"attemps of user #{userId} not found");

            return ResultWithModel<IEnumerable<AttemptResponse>>.Ok(attempts.ConvertCollection().ToList());
        }

        public async Task<ResultWithModel<AttemptResponse>> GetAttemptWithAnswers(int id)
        {
            var attempt = await _attemptRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<AttemptResponse>.Ok(attempt.Convert());
        }

        public async Task<Result> SaveAttempt(AttemptResponse attempt)
        {
            await _attemptRepository.InsertAsync(attempt);
            
            return Result.Ok();
        }

        public async Task<Result> UpdateAttempt(AttemptResponse attempt)
        {
            await _attemptRepository.UpdateAsync(attempt);
            
            return Result.Ok();
        }

        public async Task<Result> DeleteAttempt(int id)
        {
            await _attemptRepository.DeleteAsync(id); 
            
            return Result.Ok();
        }
    }
}
