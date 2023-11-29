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
    public interface IAnswerService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<AnswerRequest>>> GetAnswers();
        Task<ResultWithModel<IEnumerable<AnswerRequest>>> GetAnswersForQuestion(int questionId);
        Task<ResultWithModel<AnswerRequest>> GetAnswer(int answerId);
        
        Task<Result> CreateAnswerAsync(AnswerRequest answer);
        Task<Result> DeleteAnswerAsync(int answerId);
        Task<Result> UpdateAnswerAsync(AnswerRequest answer);


        #endregion
    }
}
