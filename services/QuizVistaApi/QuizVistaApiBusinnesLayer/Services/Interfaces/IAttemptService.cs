using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IAttemptService
    {
        Task<ResultWithModel<Attempt>> GetAttempt(int id);
        Task<ResultWithModel<Attempt>> GetAttemptWithAnswers(int id);
        Task<ResultWithModel<IEnumerable<Attempt>>> GetAttemptsOfUser(int userId);
        Result SaveAttempt(Attempt attempt);
        Result DeleteAttempt(int id);
        Result UpdateAttempt(Attempt attempt);

    }
}
