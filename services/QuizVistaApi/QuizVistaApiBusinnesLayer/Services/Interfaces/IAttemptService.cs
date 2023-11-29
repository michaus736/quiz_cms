using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Responses;
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
        Task<ResultWithModel<AttemptResponse>> GetAttempt(int id);
        Task<ResultWithModel<AttemptResponse>> GetAttemptWithAnswers(int id);
        Task<ResultWithModel<IEnumerable<AttemptResponse>>> GetAttemptsOfUser(int userId);
        Task<Result> SaveAttempt(AttemptResponse attempt);
        Task<Result> DeleteAttempt(int id);
        Task<Result> UpdateAttempt(AttemptResponse attempt);

    }
}
