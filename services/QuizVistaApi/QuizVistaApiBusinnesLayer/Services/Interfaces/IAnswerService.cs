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

        Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswers();
        Task<ResultWithModel<IEnumerable<AnswerResponse>>> GetAnswersForQuestion(AnswerResponse answerResponse);
        Task<ResultWithModel<AnswerResponse>> GetAnswer(int answerId);
        
        Task<Result> CreateAnswerAsync(AnswerResponse answer);
        Task<Result> DeleteAnswerAsync(int answerId);
        Task<Result> UpdateAnswerAsync(AnswerResponse answer);


        #endregion
    }
}
