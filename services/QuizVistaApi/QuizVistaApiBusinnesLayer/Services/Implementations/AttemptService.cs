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
    public class AttemptService : IAttemptService
    {
        private readonly IRepository<Attempt> _attemptRepository;

        public AttemptService(IRepository<Attempt> attemptRepository)
        {
            _attemptRepository = attemptRepository;
        }

        public async Task<ResultWithModel<Attempt>> GetAttempt(int id)
        {
            var attempt = await _attemptRepository.GetAsync(id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<Attempt>.Ok(attempt);

        }

        public async Task<ResultWithModel<IEnumerable<Attempt>>> GetAttemptsOfUser(int userId)
        {
            var attempts = await _attemptRepository.GetAll()
                .Where(x=>x.UserId == userId)
                .ToListAsync();

            if (attempts is null)
                throw new ArgumentException($"attemps of user #{userId} not found");

            return ResultWithModel<IEnumerable<Attempt>>.Ok(attempts);
        }

        public async Task<ResultWithModel<Attempt>> GetAttemptWithAnswers(int id)
        {
            var attempt = await _attemptRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (attempt is null)
                throw new ArgumentException($"attempt #{id} not found");

            return ResultWithModel<Attempt>.Ok(attempt);
        }

        public Result SaveAttempt(Attempt attempt)
        {
            _attemptRepository.InsertAsync(attempt);
            return Result.Ok();
        }

        public Result UpdateAttempt(Attempt attempt)
        {
            _attemptRepository.UpdateAsync(attempt);
            return Result.Ok();
        }

        public Result DeleteAttempt(int id)
        {
            _attemptRepository.DeleteAsync(id); 
            return Result.Ok();
        }
    }
}
