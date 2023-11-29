﻿using Microsoft.EntityFrameworkCore;
using QuizVistaApiBusinnesLayer.Extensions;
using QuizVistaApiBusinnesLayer.Models;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiBusinnesLayer.Services.Interfaces;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Result> CreateQuestionAsync(QuestionResponse question)
        {
            await _questionRepository.InsertAsync(question);
        }

        public async Task<Result> DeleteQuestionAsync(int questionId)
        {
            await _questionRepository.DeleteAsync(questionId);

            return Result.Ok();
        }

        public async Task<ResultWithModel<QuestionResponse>> GetQuestion(int questionId)
        {
            var question = await _questionRepository.GetAsync(questionId);

            if(question is null) 
                throw new ArgumentNullException($"question #{questionId} not found");

            return ResultWithModel<QuestionResponse>.Ok(question.Convert());
        }

        public async Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestions()
        {
            var questions = await _questionRepository.GetAll()
                .OrderBy(x => x.Id)
                .ToListAsync();

            return ResultWithModel<IEnumerable<QuestionResponse>>.Ok(questions.ConvertCollection().ToList());
        }

        public async Task<ResultWithModel<IEnumerable<QuestionResponse>>> GetQuestionsForQuiz(int quizId)
        {
            var questions = await _questionRepository.GetAll()
                .Where(x=>x.QuizId == quizId)
                .OrderBy(x=> x.Id)
                .ToListAsync();

            if(questions is null)
                throw new ArgumentNullException($"questions for quiz #{quizId} not found");

            return ResultWithModel<IEnumerable<QuestionResponse>>.Ok(questions.ConvertCollection().ToList());

        }

        public Task<ResultWithModel<QuestionResponse>> GetQuestionWithAnswers(int questionId)
        {
            var questionExtended = _questionRepository.GetAll()
                .Include(x => x.Answers)
                .FirstOrDefault(x => x.Id == questionId);

            if(questionExtended is null)
                throw new ArgumentNullException($"question #{questionId} not found");

            return Task.FromResult(ResultWithModel<QuestionResponse>.Ok(questionExtended.Convert()));

        }

        public async Task<Result> UpdateQuestionAsync(QuestionResponse question)
        {
            await _questionRepository.UpdateAsync(question);

            return Result.Ok();
        }
    }
}
