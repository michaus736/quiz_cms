using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IQuestionService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<Question>>> GetQuestions();
        Task<ResultWithModel<IEnumerable<Question>>> GetQuestionsForQuiz(int quizId);
        Task<ResultWithModel<Question>> GetQuestion(int questionId);
        Task<ResultWithModel<Question>> GetQuestionWithAnswers(int questionId);
        Task CreateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);
        Task UpdateQuestionAsync(Question question);
        
        

        #endregion
    }
}
