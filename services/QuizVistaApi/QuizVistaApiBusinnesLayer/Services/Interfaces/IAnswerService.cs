using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IAnswerService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<Answer>>> GetAnswers();
        Task<ResultWithModel<IEnumerable<Answer>>> GetAnswersForQuestion(int questionId);
        Task<ResultWithModel<Answer>> GetAnswer(int answerId);
        
        Task CreateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(int answerId);
        Task UpdateAnswerAsync(Answer answer);


        #endregion
    }
}
