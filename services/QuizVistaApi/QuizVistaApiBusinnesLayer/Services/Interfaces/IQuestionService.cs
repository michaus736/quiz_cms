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

        Task<ModelWithResult<IEnumerable<Question>>> GetQuestions();
        Task<ModelWithResult<IEnumerable<Question>>> GetQuestionsForQuiz(int quizId);
        Task<ModelWithResult<Question>> GetQuestion(int questionId);
        Task<ModelWithResult<Question>> GetQuestionWithAnswers(int questionId);
        Task CreateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);
        Task UpdateQuestionAsync(Question question);
        
        

        #endregion
    }
}
