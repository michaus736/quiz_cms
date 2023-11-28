using QuizVistaApiBusinnesLayer.Models;
using System;
using System.Collections.Generic;
using QuizVistaApiInfrastructureLayer.Entities;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IQuizService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<Quiz>>> GetQuizesAsync();
        Task<ResultWithModel<Quiz>> GetQuizAsync(int id);
        Task CreateQuizAsync(Quiz quizToCreate);
        Task DeleteQuizAsync(int idToDelete);
        Task UpdateQuizAsync(Quiz quizToUpdate);

        #endregion
        Task<ResultWithModel<Quiz>> GetQuizWithQuestionsAsync(int id);

    }
}
