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

        Task<ModelWithResult<IEnumerable<Quiz>>> GetQuizesAsync();
        Task<ModelWithResult<Quiz>> GetQuizAsync(int id);
        Task CreateQuizAsync(Quiz quizToCreate);
        Task DeleteQuizAsync(int idToDelete);
        Task UpdateQuizAsync(Quiz quizToUpdate);

        #endregion
        Task<ModelWithResult<Quiz>> GetQuizWithQuestionsAsync(int id);

    }
}
