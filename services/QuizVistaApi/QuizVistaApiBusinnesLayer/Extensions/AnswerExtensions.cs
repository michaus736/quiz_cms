using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class AnswerExtensions
    {

        public static AnswerResponse ToResponse(this Answer answer)
        {
            return new AnswerResponse
            (
                answer.Id,
                answer.AnswerText,
                answer.IsCorrect,
                answer.QuestionId,
                answer.AttemptId,
                answer.Attempt.ToResponse(),
                answer.Question.ToResponse(),
                answer.Attempts.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<AnswerResponse> ToCollectionResponse(this IEnumerable<Answer> answers)
        {
            return answers.Select(ToResponse) ?? new List<AnswerResponse>();
        public static IEnumerable<AnswerResponse> ConvertCollection(this IEnumerable<Answer> answers)
        {
            return answers.Select(Convert) ?? new List<AnswerResponse>();
        }

        public static Answer ToEntity(this AnswerRequest answerRequest) {
            return new Answer
            {
                AnswerText = answerRequest.AnswerText,
                Id = answerRequest.Id,
                IsCorrect = answerRequest.IsCorrect,
                QuestionId = answerRequest.QuestionId,
            };
        }

    }
}
