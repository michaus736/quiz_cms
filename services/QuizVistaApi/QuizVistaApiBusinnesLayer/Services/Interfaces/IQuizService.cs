﻿using QuizVistaApiBusinnesLayer.Models;
using System;
using System.Collections.Generic;
using QuizVistaApiInfrastructureLayer.Entities;
using System.Text;
using System.Threading.Tasks;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Models.Requests;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IQuizService
    {
        #region REST

        Task<ResultWithModel<IEnumerable<QuizResponse>>> GetQuizesAsync();
        Task<ResultWithModel<QuizResponse>> GetQuizAsync(int id);
        Task<Result>  CreateQuizAsync(string userId,QuizRequest quizToCreate);
        Task<Result> DeleteQuizAsync(int idToDelete);
        Task<Result> UpdateQuizAsync(QuizRequest quizToUpdate);
        Task<Result> AssignUser(AssignUserRequest assignUserRequest);
        Task<Result> UnAssignUser(AssignUserRequest assignUserRequest);
        //Task<Result> AssignUser(QuizRequest quizToUpdate);

        #endregion
        Task<ResultWithModel<QuizResponse>> GetQuizWithQuestionsAsync(int id);

    }
}
