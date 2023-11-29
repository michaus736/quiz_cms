using QuizVistaApiBusinnesLayer.Models;
using System;
using System.Collections.Generic;
using QuizVistaApiInfrastructureLayer.Entities;
using System.Text;
using System.Threading.Tasks;
using QuizVistaApiBusinnesLayer.Models.Responses;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IQuizService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizesAsync();
        Task<ResultWithModel<QuizResponse>> GetQuizAsync(int id);
        Task<Result>  CreateQuizAsync(QuizResponse quizToCreate);
        Task<Result> DeleteQuizAsync(int idToDelete);
        Task<Result> UpdateQuizAsync(QuizResponse quizToUpdate);

        #endregion
        Task<ResultWithModel<QuizResponse>> GetQuizWithQuestionsAsync(int id);

    }
}
